using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using Attribute = microservices.CatalogAPI.Domain.Models.Attribute;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IAttributeService
    {
        public Task<IEnumerable<AttributeResponse>> GetAllAttributes();
        public Task<Attribute> GetSingleAttributeById(Guid id);
        public Task CreateNewAttribute(AttributeRequest request);
        public Task UpdateAttribute(Guid id, AttributeRequest request);
        public Task DeleteSingleAttributeById(Guid id);
    }
}