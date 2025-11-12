using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
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

        public async Task<IEnumerable<AttributeGroupResponse>> GetAllAttributeGroups()
        {
            List<AttributeGroup> attributeGroups = await _attributeGroupDAO.GetAttributeGroups();

            IEnumerable<AttributeGroupResponse> response = attributeGroups
                .Select(attributeGroup =>
                    new AttributeGroupResponse(
                        attributeGroup.GetId(),
                        attributeGroup.GetTitle()
                    )
                );

            return response;
        }

        public async Task<AttributeGroup> GetSingleAttributeGroupById(int id)
        {
            AttributeGroup attributeGroup = await _attributeGroupDAO.GetAttributeGroupById(id);

            return attributeGroup;
        }

        public async Task<IEnumerable<AttributeGroup>> GetListAttributeGroupByIds(List<int> ids)
        {
            IEnumerable<AttributeGroup> attributeGroups = await _attributeGroupDAO.GetAttributeGroupByIds(ids);

            return attributeGroups;
        }

        public async Task<AttributeGroup> GetSingleAttributeGroupByTitle(string title)
        {
            AttributeGroup attributeGroup = await _attributeGroupDAO.GetAttributeGroupByTitle(title);

            return attributeGroup;
        }

        public async Task CreateNewAttributeGroup(AttributeGroupRequest request)
        {
            AttributeGroup newAttributeGroup = new AttributeGroup(request.Title);

            await _attributeGroupDAO.CreateAttributeGroups(newAttributeGroup);
        }

        public async Task UpdateSingleAttributeGroupById(int id, AttributeGroupRequest request)
        {
            AttributeGroup updateAttributeGroup = new AttributeGroup(id, request.Title);

            await _attributeGroupDAO.UpdateAttributeGroup(updateAttributeGroup);
        }

        public async Task DeleteSingleAttributeGroupById(int id)
        {
            await _attributeGroupDAO.DeleteAttributeGroupById(id);
        }
    }
}