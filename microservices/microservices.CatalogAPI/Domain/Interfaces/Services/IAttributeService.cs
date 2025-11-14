using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using Attribute = microservices.CatalogAPI.Domain.Models.Attribute;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IAttributeService
    {
        Task<IEnumerable<AttributeResponse>> GetAllAttributes();
        Task<Attribute> GetSingleAttributeById(Guid id);
        Task<IEnumerable<AttributeResponse>> GetListAttributeResponseByIds(List<Guid> ids);
        Task CreateNewAttribute(AttributeRequest request);
        Task UpdateAttribute(Guid id, AttributeRequest request);
        Task DeleteSingleAttributeById(Guid id);
    }
}