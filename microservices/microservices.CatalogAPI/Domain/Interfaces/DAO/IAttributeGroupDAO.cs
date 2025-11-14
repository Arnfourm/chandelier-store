using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IAttributeGroupDAO
    {
        Task<List<AttributeGroup>> GetAttributeGroups();
        Task<AttributeGroup> GetAttributeGroupById(int id);
        Task<List<AttributeGroup>> GetAttributeGroupByIds(List<int> ids);
        Task<AttributeGroup> GetAttributeGroupByTitle(string title);
        Task<int> CreateAttributeGroups(AttributeGroup attributeGroup);
        Task<int> UpdateAttributeGroup(AttributeGroup attributeGroup);
        Task DeleteAttributeGroupById(int id);
    }
}