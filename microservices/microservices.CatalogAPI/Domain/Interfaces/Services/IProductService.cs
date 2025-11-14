using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllProducts();
        Task<Product> GetSingleProductById(Guid id);
        Task<Guid> CreateNewProduct(ProductRequest request);
        Task UpdateSingleProductById(Guid id, ProductRequest request);
        Task UpdateSingleProductQuantityById(Guid id, int quantity);
        Task DeleteSingleProductById(Guid id);
    }
}