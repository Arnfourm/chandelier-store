using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IProductDAO
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(Guid id);
        Task<Guid> CreateProduct(Product product);
        Task<Guid> UpdateProduct(Product product);
        Task UpdateProductQuantityById(Guid id, int quantity);
        Task DeleteProductById(Guid id);
    }
}