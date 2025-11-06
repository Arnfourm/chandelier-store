using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetAllProducts();
        public Task<Product> GetSingleProductById(Guid id);
        public Task<Guid> CreateNewProduct(Product product);
        public Task<Guid> UpdateSingleProduct(Product product);
        public Task DeleteSingleProductById(Guid id);
    }
}