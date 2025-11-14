using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Models;

namespace microservices.CatalogAPI.Domain.Interfaces.Services
{
    public interface IAttributeGroupService
    {
        Task<IEnumerable<AttributeGroupResponse>> GetAllAttributeGroups();
        Task<AttributeGroup> GetSingleAttributeGroupById(int id);
        Task<IEnumerable<AttributeGroupResponse>> GetListAttributeGroupResponseByIds(List<int> ids);
        Task<AttributeGroup> GetSingleAttributeGroupByTitle(string title);
        Task CreateNewAttributeGroup(AttributeGroupRequest request);
        Task UpdateSingleAttributeGroupById(int id, AttributeGroupRequest request);
        Task DeleteSingleAttributeGroupById(int id);
    }
}