using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductResponse>> GetAllProducts();
        public Task<Product> GetSingleProductById(Guid id);
        public Task<Guid> CreateNewProduct(ProductRequest request);
        public Task UpdateSingleProductById(Guid id, ProductRequest request);
        public Task UpdateSingleProductQuantityById(Guid id, int quantity);
        public Task DeleteSingleProductById(Guid id);
    }
}