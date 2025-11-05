using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IAttributeGroupService
    {
        public Task<List<AttributeGroup>> GetAllAttributeGroups();
        public Task<AttributeGroup> GetSingleAttributeGroupById(int id);
        public Task<int> CreateNewAttributeGroup(AttributeGroup attributeGroup);
        public Task DeleteSingleAttributeGroupById(int id);
    }
}