namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IAttributeGroupService
    {
        public Task<List<AttributeGroup>> GetAllAttributeGroups();
        public Task<AttributeGroup> GetSingleAttributeGroupById(ind id);
        public Task<int> CreateNewAttributeGroup(AttributeGroup attributeGroup);
        public Task DeleteSingleAttributeGroupById(int id);
    }
}