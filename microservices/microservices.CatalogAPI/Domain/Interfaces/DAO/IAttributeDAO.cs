using Attribute = microservices.CatalogAPI.Domain.Models.Attribute;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IAttributeDAO
    {
        Task<List<Attribute>> GetAttributes();
        Task<Attribute> GetAttributeById(Guid id);
        Task<List<Attribute>> GetAttributeByIds(List<Guid> ids);
        Task<Guid> CreateAttribute(Attribute attribute);
        Task<Guid> UpdateAttribute(Attribute attribute);
        Task DeleteAttributeById(Guid id);
    }
}