using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace microservices.CatalogAPI.API.Controllers
{
    [ApiController]
    [Route("api/Catalog/[controller]")]
    public class AttributeController : ControllerBase
    {
        private readonly IAttributeService _attributeService;

        public AttributeController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeResponse>>> GetAttributes()
        {
            IEnumerable<AttributeResponse> response = await _attributeService.GetAllAttributes();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAttribute([FromBody] AttributeRequest request)
        {
            await _attributeService.CreateNewAttribute(request);

            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateAttribute(Guid id, [FromBody] AttributeRequest request)
        {
            await _attributeService.UpdateAttribute(id, request);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteAttribute(Guid id)
        {
            await _attributeService.DeleteSingleAttributeById(id);

            return Ok();
        }

    }
}
