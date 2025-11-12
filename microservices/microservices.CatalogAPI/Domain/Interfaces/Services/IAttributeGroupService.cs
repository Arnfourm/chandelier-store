using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IAttributeGroupService
    {
        public Task<IEnumerable<AttributeGroupResponse>> GetAllAttributeGroups();
        public Task<AttributeGroup> GetSingleAttributeGroupById(int id);
        public Task<IEnumerable<AttributeGroup>> GetListAttributeGroupByIds(List<int> ids);
        public Task<AttributeGroup> GetSingleAttributeGroupByTitle(string title);
        public Task CreateNewAttributeGroup(AttributeGroupRequest request);
        Task UpdateSingleAttributeGroupById(int id, AttributeGroupRequest request);
        public Task DeleteSingleAttributeGroupById(int id);
    }
}