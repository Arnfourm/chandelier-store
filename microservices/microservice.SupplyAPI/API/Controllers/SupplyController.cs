using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.API.Contracts.Responses;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace microservice.SupplyAPI.API.Controllers
{
    [ApiController]
    [Route("api/Supply/[controller]")]
    public class SupplyController : ControllerBase
    {
        private readonly ISupplyService _supplyService;

        public SupplyController(ISupplyService supplyService)
        {
            _supplyService = supplyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplyResponse>>> GetSupplies()
        {
            IEnumerable<SupplyResponse> response = await _supplyService.GetAllSupplies();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSupply([FromBody] SupplyRequest request)
        {
            await _supplyService.CreateNewSupply(request);

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteSupply(Guid id)
        {
            await _supplyService.DeleteSingleSupplyById(id);

            return Ok();
        }
    }
}
