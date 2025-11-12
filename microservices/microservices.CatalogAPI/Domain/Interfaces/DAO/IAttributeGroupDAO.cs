using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.DAO
{
    public interface IAttributeGroupDAO
    {
        public Task<List<AttributeGroup>> GetAttributeGroups();
        public Task<AttributeGroup> GetAttributeGroupById(int id);
        public Task<List<AttributeGroup>> GetAttributeGroupByIds(List<int> ids);
        public Task<AttributeGroup> GetAttributeGroupByTitle(string title);
        public Task<int> CreateAttributeGroups(AttributeGroup attributeGroup);
        public Task<int> UpdateAttributeGroup(AttributeGroup attributeGroup);
        public Task DeleteAttributeGroupById(int id);
    }
}