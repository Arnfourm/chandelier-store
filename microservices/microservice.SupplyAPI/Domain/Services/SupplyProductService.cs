using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.API.Contracts.Responses;
using microservice.SupplyAPI.Domain.DTO.Requests;
using microservice.SupplyAPI.Domain.DTO.Responses;
using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using microservice.SupplyAPI.Domain.Models;
using microservices.SupplyAPI.Domain.Interfaces.Services;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace microservice.SupplyAPI.Domain.Services
{
    public class SupplyProductService : ISupplyProductService
    {
        private readonly ISupplyProductDAO _supplyProductDAO;

        private readonly ISupplyService _supplyService;
        private readonly ITokenService _tokenService;

        private readonly string _catalogMicroservice;

        public SupplyProductService(
            ISupplyProductDAO supplyProductDAO, 
            ISupplyService supplyService,
            ITokenService tokenService,
            IConfiguration catalogMicroservice
        )
        {
            _supplyProductDAO = supplyProductDAO;

            _supplyService = supplyService;
            _tokenService = tokenService;

            _catalogMicroservice = catalogMicroservice["Microservices:CatalogMicroservice:Url"] 
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

                    List<ProductResponseDTO> productResponse = await responseProducts.Content.ReadFromJsonAsync<List<ProductResponseDTO>>()
                        ?? new List<ProductResponseDTO>();

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
            string token = await _tokenService.GetTokenAsync();

            ProductRequestDTO productRequestDTO;

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    var productRequest = new HttpRequestMessage(HttpMethod.Get, $"{_catalogMicroservice}/Product/{request.ProductId}");

                    productRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage responseProduct = await httpClient.SendAsync(productRequest);

                    if (!responseProduct.IsSuccessStatusCode)
                    {
                        throw new Exception($"Exception: product with id: {request.ProductId} doesn't exit");
                    }

                    ProductResponseDTO productResponse = await responseProduct.Content.ReadFromJsonAsync<ProductResponseDTO>()
                        ?? throw new Exception($"Exception: product with id: {request.ProductId} can't be null");

                    productRequestDTO = new ProductRequestDTO
                    {
                        Article = productResponse.Article,
                        Title = productResponse.Title,
                        Price = productResponse.Price,
                        Quantity = productResponse.Quantity + request.Quantity,
                        LampPower = productResponse.LampPower,
                        LampCount = productResponse.LampCount,
                        ProductTypeId = productResponse.ProductType.Id,
                    };
                }
            }

            SupplyProduct newSupplyProduct = new SupplyProduct(request.SupplyId, request.ProductId, request.Quantity);

            await _supplyProductDAO.CreateSupplyProduct(newSupplyProduct);

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    var formData = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("Article", productRequestDTO.Article),
                        new KeyValuePair<string, string>("Title", productRequestDTO.Title),
                        new KeyValuePair<string, string>("Price", productRequestDTO.Price.ToString()),
                        new KeyValuePair<string, string>("Quantity", productRequestDTO.Quantity.ToString()),
                        new KeyValuePair<string, string>("LampPower", productRequestDTO.LampPower.ToString() ?? ""),
                        new KeyValuePair<string, string>("LampCount", productRequestDTO.LampCount.ToString() ?? ""),
                        new KeyValuePair<string, string>("ProductTypeId", productRequestDTO.ProductTypeId.ToString()),
                    });

                    var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"{_catalogMicroservice}/Product/{request.ProductId}")
                    {
                        Content = formData
                    };

                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage responsePut = await httpClient.SendAsync(requestMessage);

                    if (!responsePut.IsSuccessStatusCode)
                    {
                        await DeleteSupplyProductByBothIds(request.SupplyId, request.ProductId);

                        throw new Exception($"Exception: can't update price of product: {request.ProductId}");
                    }
                }
            }
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

            string token = await _tokenService.GetTokenAsync();

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    var productRequest = new HttpRequestMessage(HttpMethod.Get, $"{_catalogMicroservice}/Product/{productId}");

                    productRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage responseProduct = await httpClient.SendAsync(productRequest);

                    if (!responseProduct.IsSuccessStatusCode)
                    {
                        throw new Exception($"Product with id {productId} doesn't exist");
                    }
                }
            }

            await _supplyProductDAO.DeleteSupplyProductByBothIds(supplyId, productId);
        }
    }
}
