using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace microservices.CatalogAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttributeGroupController : ControllerBase
    {
        private readonly IAttributeGroupService _attributeGroupService;

        public AttributeGroupController(IAttributeGroupService attributeGroupService)
        {
            _attributeGroupService = attributeGroupService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeGroupResponse>>> GetAttributeGroups()
        {
            IEnumerable<AttributeGroupResponse> response = await _attributeGroupService.GetAllAttributeGroups();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAttributeGroup([FromBody] AttributeGroupRequest request)
        {
            await _attributeGroupService.CreateNewAttributeGroup(request);

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAttributeGroup(int id, AttributeGroupRequest request)
        {
            await _attributeGroupService.UpdateSingleAttributeGroupById(id, request);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAttributeGroup(int id)
        {
            await _attributeGroupService.DeleteSingleAttributeGroupById(id);

            return Ok();
        }
    }
}
