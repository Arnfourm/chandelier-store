using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace microservices.CatalogAPI.API.Controllers
{
    [ApiController]
    [Route("api/Catalog/[controller]")]
    public class AttributeGroupController : ControllerBase
    {
        private readonly IAttributeGroupService _attributeGroupService;

        public AttributeGroupController(IAttributeGroupService attributeGroupService)
        {
            _attributeGroupService = attributeGroupService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AttributeGroupResponse>>> GetAttributeGroups()
        {
            IEnumerable<AttributeGroupResponse> response = await _attributeGroupService.GetAllAttributeGroups();

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> CreateAttributeGroup([FromBody] AttributeGroupRequest request)
        {
            await _attributeGroupService.CreateNewAttributeGroup(request);

            return Ok();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> UpdateAttributeGroup(int id, AttributeGroupRequest request)
        {
            await _attributeGroupService.UpdateSingleAttributeGroupById(id, request);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> DeleteAttributeGroup(int id)
        {
            await _attributeGroupService.DeleteSingleAttributeGroupById(id);

            return Ok();
        }
    }
}
