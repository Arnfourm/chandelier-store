using System.ComponentModel.DataAnnotations;
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

    private readonly IWebHostEnvironment _env;
    public ProductService(
        IProductDAO productDAO,
        IProductTypeService productTypeService,
        IDeleteProductAttributeService deleteProductAttributeService,
        IWebHostEnvironment env
    )
    {
        _productDAO = productDAO;

        _productTypeService = productTypeService;
        _deleteProductAttributeService = deleteProductAttributeService;

        _env = env;
    }

    public async Task<IEnumerable<ProductResponse>> GetAllProducts(
            string? sort,
            ProductFilter filters,
            string? search
        )
    {
        List<Product> products = await _productDAO.GetProducts(sort, filters, search);

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
                product.GetLampPower(),
                product.GetLampCount(),
                productTypeResponse,
                product.GetMainImgPath(),
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
            product.GetLampPower(),
            product.GetLampCount(),
            productTypeResponse,
            product.GetMainImgPath(),
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
                product.GetLampPower(),
                product.GetLampCount(),
                productTypeResponse,
                product.GetMainImgPath(),
                product.GetAddedDate()
            );
        });

        return response;
    }

    public async Task<Guid> CreateNewProduct(ProductRequest request)
    {
        ProductType productType = await _productTypeService.GetSingleProductTypeById(request.ProductTypeId);

        string? imagePath = await ResolveImage(request);

        Product newProduct = new Product(
            request.Article,
            request.Title,
            request.Price,
            request.Quantity,
            request.LampPower,
            request.LampCount,
            productType.GetId(),
            imagePath,
            DateOnly.FromDateTime(DateTime.Now)
        );

        Guid productId = await _productDAO.CreateProduct(newProduct);

        return productId;
    }

    public async Task UpdateSingleProductById(Guid id, ProductRequest request)
    {
        ProductType productType = await _productTypeService.GetSingleProductTypeById(request.ProductTypeId);
        Product product = await GetSingleProductById(id);

        string? imagePath = await ResolveImage(request);

        Product updateProduct = new Product
        (
            id,
            request.Article,
            request.Title,
            request.Price,
            request.Quantity,
            request.LampPower,
            request.LampCount,
            productType.GetId(),
            imagePath,
            product.GetAddedDate()
        );

        await _productDAO.UpdateProduct(updateProduct);
    }

    public async Task DeleteSingleProductById(Guid id)
    {
        await _deleteProductAttributeService.DeleteListProductAttributesByProductId(id);

        await _productDAO.DeleteProductById(id);
    }

    private async Task<string?> ResolveImage(ProductRequest request)
    {        
        if (request.Image != null && request.Image.Length > 0)
        { 
            string[] extentionsList = [".png", ".jpg", ".jpeg"];
            string imagesDir = "images";
            string imageFilename = request.Image.FileName.ToLower();

            if (!extentionsList.Contains(Path.GetExtension(imageFilename)))
            {
                throw new ValidationException("Image should be with extension .png, .jpg or .jpeg");
            }

            var uploadDir = Path.Combine(_env.WebRootPath, imagesDir);
            string filename = $"{request.Article}-{imageFilename}";
                
            var filePath = Path.Combine(uploadDir, filename);

            if (!File.Exists(filePath))
            {
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                        await request.Image.CopyToAsync(fileStream);
                    } 
                    catch (Exception ex)
                    {
                        throw new ArgumentException("Exception while saving image to server: ", ex);
                    }
                }
            }

            return Path.Combine(imagesDir, filename).Replace("\\", "/");   
        }
        else
        {
            return null;
        }
    }
}