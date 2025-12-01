using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.API.Filters;
using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductDAO _productDAO;

    private readonly IProductTypeService _productTypeService;
    private readonly IDeleteProductAttributeService _deleteProductAttributeService;

    public ProductService(
        IProductDAO productDAO,
        IProductTypeService productTypeService,
        IDeleteProductAttributeService deleteProductAttributeService
    )
    {
        _productDAO = productDAO;

        _productTypeService = productTypeService;
        _deleteProductAttributeService = deleteProductAttributeService;
    }

    public async Task<IEnumerable<ProductResponse>> GetAllProducts(
            string? sort,
            ProductFilter filters
        )
    {
        List<Product> products = await _productDAO.GetProducts(sort, filters);

        List<int> productTypeIds = products.Select(product => product.GetProductTypeId()).ToList();

        IEnumerable<ProductTypeResponse> productTypes = await _productTypeService.GetListProductTypeResponseByIds(productTypeIds);

        var productTypeDict = productTypes.ToDictionary(productType => productType.Id);

        IEnumerable<ProductResponse> response = products.Select(product =>
        {
            ProductTypeResponse productTypeResponse = productTypeDict[product.GetProductTypeId()];

            return new ProductResponse(
                product.GetId(),
                product.GetArticle(),
                product.GetTitle(),
                product.GetPrice(),
                product.GetQuantity(),
                productTypeResponse,
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

    public async Task<ProductResponse> GetSingleProductResponseById(Guid id)
    {
        Product product = await _productDAO.GetProductById(id);

        ProductTypeResponse productTypeResponse = await _productTypeService.GetSingleProductTypeResponseById(product.GetProductTypeId());

        ProductResponse response = new ProductResponse
        (
            product.GetId(),
            product.GetArticle(),
            product.GetTitle(),
            product.GetPrice(),
            product.GetQuantity(),
            productTypeResponse,
            product.GetAddedDate()
        );

        return response;
    }

    public async Task<IEnumerable<ProductResponse>> GetListProductResponseByIds(List<Guid> ids)
    {
        List<Product> products = await _productDAO.GetProductsByIds(ids);

        List<int> productTypeIds = products.Select(product => product.GetProductTypeId()).ToList();

        IEnumerable<ProductTypeResponse> productTypes = await _productTypeService.GetListProductTypeResponseByIds(productTypeIds);

        var productTypeDict = productTypes.ToDictionary(productType => productType.Id);

        IEnumerable<ProductResponse> response = products.Select(product =>
        {
            ProductTypeResponse productTypeResponse = productTypeDict[product.GetProductTypeId()];

            return new ProductResponse(
                product.GetId(),
                product.GetArticle(),
                product.GetTitle(),
                product.GetPrice(),
                product.GetQuantity(),
                productTypeResponse,
                product.GetAddedDate()
            );
        });

        return response;
    }

    public async Task<Guid> CreateNewProduct(ProductRequest request)
    {
        ProductType productType = await _productTypeService.GetSingleProductTypeById(request.ProductTypeId);

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
        ProductType productType = await _productTypeService.GetSingleProductTypeById(request.ProductTypeId);

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

    public async Task DeleteSingleProductById(Guid id)
    {
        await _deleteProductAttributeService.DeleteListProductAttributesByProductId(id);

        await _productDAO.DeleteProductById(id);
    }
}