using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllProducts(string? sort, string? product_type, int? price_min, int? price_max, string? room_type, string? color, string? lamp_power, string? lamp_count);
        Task<Product> GetSingleProductById(Guid id);
        Task<ProductResponse> GetSingleProductResponseById(Guid id);
        Task<IEnumerable<ProductResponse>> GetListProductResponseByIds(List<Guid> ids);
        Task<Guid> CreateNewProduct(ProductRequest request);
        Task UpdateSingleProductById(Guid id, ProductRequest request);
        Task DeleteSingleProductById(Guid id);
    }
}