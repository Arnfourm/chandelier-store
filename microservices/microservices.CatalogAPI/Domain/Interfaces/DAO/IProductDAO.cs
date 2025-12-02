using microservices.CatalogAPI.API.Filters;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IProductDAO
    {
        Task<List<Product>> GetProducts(string? sort, ProductFilter filters, string? search);
        Task<Product> GetProductById(Guid id);
        Task<List<Product>> GetProductsByIds(List<Guid> ids);
        Task<Guid> CreateProduct(Product product);
        Task<Guid> UpdateProduct(Product product);
        Task DeleteProductById(Guid id);
    }
}