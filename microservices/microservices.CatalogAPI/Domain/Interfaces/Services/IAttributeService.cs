using Attribute = microservices.CatalogAPI.Domain.Models.Attribute;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IAttributeService
    {
        public Task<List<Attribute>> GetAllAttributes();
        public Task<Attribute> GetSingleAttributeById(Guid id);
        public Task<Guid> CreateNewAttribute(Attribute attribute);
        public Task DeleteSingleAttributeById(Guid id);
    }
}