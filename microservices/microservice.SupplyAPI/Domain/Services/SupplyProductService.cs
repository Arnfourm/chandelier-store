using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.API.Contracts.Responses;
using microservice.SupplyAPI.Domain.DTO;
using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using microservice.SupplyAPI.Domain.Models;

namespace microservice.SupplyAPI.Domain.Services
{
    public class SupplyProductService : ISupplyProductService
    {
        private readonly ISupplyProductDAO _supplyProductDAO;

        private readonly ISupplyService _supplyService;

        private readonly string _catalogMicroservice;

        public SupplyProductService(
            ISupplyProductDAO supplyProductDAO, 
            ISupplyService supplyService,
            IConfiguration catalogMicroservice
        )
        {
            _supplyProductDAO = supplyProductDAO;

            _supplyService = supplyService;

            _catalogMicroservice = catalogMicroservice["Microservices:CatalogMicroservice"] 
                ?? throw new ArgumentException("Catalog microservice is null");
        }

        public async Task<IEnumerable<SupplyProductResponse>> GetListSupplyProductBySupplyId(Guid supplyId)
        {
            List<SupplyProduct> supplyProducts = await _supplyProductDAO.GetSupplyProductBySupplyId(supplyId);

            string productIds = string.Join("&", supplyProducts.Select(supplyProduct => $"ids={supplyProduct.GetProductId()}"));

            IEnumerable<Product> products;

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    HttpResponseMessage responseProducts = await httpClient.GetAsync($"{_catalogMicroservice}/Product/ByIds?{productIds}");

                    if (!responseProducts.IsSuccessStatusCode)
                    {
                        throw new Exception("Failed then tried to get request from Catalog service");
                    };

                    List<ProductDTO> productResponse = await responseProducts.Content.ReadFromJsonAsync<List<ProductDTO>>()
                        ?? new List<ProductDTO>();

                    products = productResponse
                        .Select(productDTO => new Product
                        (
                            productDTO.Id,
                            productDTO.Article,
                            productDTO.Title,
                            productDTO.Price
                        ));
                }
            }

            var productDict = products.ToDictionary(product => product.GetId());

            IEnumerable<SupplyProductResponse> response = supplyProducts
                .Select(supplyProduct =>
                    {
                        Product product = productDict[supplyProduct.GetProductId()];

                        return new SupplyProductResponse
                        (
                            new ProductResponse
                            (
                                product.GetId(),
                                product.GetArticle(),
                                product.GetTitle(),
                                product.GetPrice()
                            ),
                            supplyProduct.GetQuantity()
                        );
                    }
                );

            return response;
        }

        public async Task CreateNewSupplyProduct(SupplyProductRequest request)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    HttpResponseMessage productResponse = await httpClient.GetAsync($"{_catalogMicroservice}/Product/{request.ProductId}");

                    if (!productResponse.IsSuccessStatusCode)
                    {
                        throw new Exception($"Exception: product with id: {request.ProductId} doesn't exit");
                    }
                }
            }

            SupplyProduct newSupplyProduct = new SupplyProduct(request.SupplyId, request.ProductId, request.Quantity);

            await _supplyProductDAO.CreateSupplyProduct(newSupplyProduct);
        }

        public async Task DeleteSupplyProductBySupplyId(Guid supplyId)
        {
            Supply supply = await _supplyService.GetSingleSupplyById(supplyId);

            if (supply == null)
            {
                throw new Exception($"Supply with id {supplyId} doesn't exist");
            }

            await _supplyProductDAO.DeleteSupplyProductBySupplyId(supplyId);
        }

        public async Task DeleteSupplyProductByBothIds(Guid supplyId, Guid productId)
        {
            Supply supply = await _supplyService.GetSingleSupplyById(supplyId);

            if (supply == null)
            {
                throw new Exception($"Supply with id {supplyId} doesn't exist");
            }

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    HttpResponseMessage responseProducts = await httpClient.GetAsync($"{_catalogMicroservice}/Product/{productId}");

                    if (!responseProducts.IsSuccessStatusCode)
                    {
                        throw new Exception($"Product with id {productId} doesn't exist");
                    }
                }
            }

            await _supplyProductDAO.DeleteSupplyProductByBothIds(supplyId, productId);
        }
    }
}
