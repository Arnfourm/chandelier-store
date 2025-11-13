using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IProductAttributeService
    {
        public Task<IEnumerable<ProductAttributeResponse>> GetProductAttributeByProductId(Guid productId);
        public Task CreateNewSingleProductAttribute(ProductAttributeRequest request);
        public Task UpdateSingleProductAttribute(ProductAttributeRequest request);
    }
}
