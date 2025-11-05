using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Services
{
    public class AttributeGroupService : IAttributeGroupService
    {
        private readonly IAttributeGroupDAO _attributeGroupDAO;

        public AttributeGroupService(IAttributeGroupDAO attributeGroupDAO)
        {
            _attributeGroupDAO = attributeGroupDAO;       
        }

        public async Task<List<AttributeGroup>> GetAllAttributeGroups()
        {
            List<AttributeGroup> attributeGroups = await _attributeGroupDAO.GetAttributeGroups();

            return attributeGroups;
        }

        public async Task<AttributeGroup> GetSingleAttributeGroupById(int id)
        {
            AttributeGroup attributeGroup = await _attributeGroupDAO.GetAttributeGroupById(id);

            return attributeGroup;
        }

        public async Task<int> CreateNewAttributeGroup(AttributeGroup attributeGroup)
        {
            int attributeGroupId = await _attributeGroupDAO.CreateAttributeGroups(attributeGroup);

            return attributeGroupId;
        }

        public async Task DeleteSingleAttributeGroupById(int id)
        {
            await _attributeGroupDAO.DeleteAttributeGroupById(id);
        }
    }
}