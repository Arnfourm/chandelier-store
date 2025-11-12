using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductDAO _productDAO;

    private readonly IProductTypeService _productTypeService;

    public ProductService(IProductDAO productDAO, IProductTypeService productTypeService)
    {
        _productDAO = productDAO;

        _productTypeService = productTypeService;
    }

    public async Task<IEnumerable<ProductResponse>> GetAllProducts()
    {
        List<Product> products = await _productDAO.GetProducts();

        List<int> productTypeIds = products.Select(product => product.GetProductTypeId()).ToList();

        List<ProductType> productTypes = await _productTypeService.GetListProductTypeByIds(productTypeIds);

        var productTypeDict = productTypes.ToDictionary(productType => productType.GetId());
        
        IEnumerable<ProductResponse> response = products.Select(product =>
        {
            var productType = productTypeDict[product.GetProductTypeId()];

            return new ProductResponse(
                product.GetId(),
                product.GetArticle(),
                product.GetTitle(),
                product.GetPrice(),
                product.GetQuantity(),
                productType.GetTitle(),
                product.GetAddedDate()
            );
        });

        return response;
    }

    public async Task<Product> GetSingleProductById(Guid id)
    {
        Product product = await _productDAO.GetProductById(id);

        return product;
    }

    public async Task<Guid> CreateNewProduct(ProductRequest request)
    {
        ProductType productType = await _productTypeService.GetSingleProductTypeByTitle(request.ProductType);

        Product newProduct = new Product(
            request.Article,
            request.Title,
            request.Price,
            request.Quantity,
            productType.GetId(),
            DateOnly.FromDateTime(DateTime.Now)
        );

        Guid productId = await _productDAO.CreateProduct(newProduct);

        return productId;
    }

    public async Task UpdateSingleProductById(Guid id, ProductRequest request)
    {
        ProductType productType = await _productTypeService.GetSingleProductTypeByTitle(request.ProductType);

        Product updateProduct = new Product
        (
            id,
            request.Article,
            request.Title,
            request.Price,
            request.Quantity,
            productType.GetId(),
            DateOnly.FromDateTime(DateTime.Now)
        );

        await _productDAO.UpdateProduct(updateProduct);
    }

    public async Task UpdateSingleProductQuantityById(Guid id, int quantity)
    {
        await _productDAO.UpdateProductQuantityById(id, quantity);
    }

    public async Task DeleteSingleProductById(Guid id)
    {
        await _productDAO.DeleteProductById(id);
    }
}