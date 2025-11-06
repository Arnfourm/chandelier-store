using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IProductDAO
    {
        public Task<List<Product>> GetProducts();
        public Task<Product> GetProductById(Guid id);
        public Task<Guid> CreateProduct(Product product);
        public Task DeleteProductById(Guid id);
    }
}
