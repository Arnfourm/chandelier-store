using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IProductDAO
    {
        Task<List<Product>> GetProducts(string? sort, string? product_type, int? price_min, int? price_max, string? room_type, string? color, string? lamp_power, string? lamp_count);
        Task<Product> GetProductById(Guid id);
        Task<List<Product>> GetProductsByIds(List<Guid> ids);
        Task<Guid> CreateProduct(Product product);
        Task<Guid> UpdateProduct(Product product);
        Task DeleteProductById(Guid id);
    }
}