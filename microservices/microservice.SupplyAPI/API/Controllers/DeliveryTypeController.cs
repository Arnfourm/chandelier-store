using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.API.Contracts.Responses;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace microservice.SupplyAPI.API.Controllers
{
    [ApiController]
    [Route("api/Supply/[controller]")]
    public class DeliveryTypeController : ControllerBase
    {
        private readonly IDeliveryTypeService _deliveryTypeService;

        public DeliveryTypeController(IDeliveryTypeService service)
        {
            _deliveryTypeService = service;
        }

        [HttpGet]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult<IEnumerable<DeliveryTypeResponse>>> GetDeliveryTypes()
        {
            IEnumerable<DeliveryTypeResponse> response = await _deliveryTypeService.GetAllDeliveryType();

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> CreateDeliveryType([FromBody] DeliveryTypeRequest request)
        {
            DeliveryTypeResponse response = await _deliveryTypeService.CreateNewDeliveryType(request);

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> UpdateDeliveryType(int id, [FromBody] DeliveryTypeRequest request)
        {
            await _deliveryTypeService.UpdateSingleDeliveryTypeById(id, request);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> DeleteDeliveryType(int id)
        {
            await _deliveryTypeService.DeleteSingleDeliveryTypeById(id);

            return Ok();
        }
    }
}
