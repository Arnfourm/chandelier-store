using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IProductAttributeService
    {
        Task<IEnumerable<ProductAttributeResponse>> GetProductAttributeByProductId(Guid productId);
        Task CreateNewSingleProductAttribute(ProductAttributeRequest request);
        Task UpdateSingleProductAttribute(ProductAttributeRequest request);
    }
}
