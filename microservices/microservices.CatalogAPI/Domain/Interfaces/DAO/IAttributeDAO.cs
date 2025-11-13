using Attribute = microservices.CatalogAPI.Domain.Models.Attribute;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IAttributeDAO
    {
        public Task<List<Attribute>> GetAttributes();
        public Task<Attribute> GetAttributeById(Guid id);
        public Task<List<Attribute>> GetAttributeByIds(List<Guid> ids);
        public Task<Guid> CreateAttribute(Attribute attribute);
        public Task<Guid> UpdateAttribute(Attribute attribute);
        public Task DeleteAttributeById(Guid id);
    }
}