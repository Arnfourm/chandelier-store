using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace microservices.OrderAPI.API.Controllers
{
    [ApiController]
    [Route("api/Order/[controller]")]
    public class DeliveryTypeController : ControllerBase
    {
        private readonly IDeliveryTypeService _deliveryTypeService;

        public DeliveryTypeController(IDeliveryTypeService deliveryTypeService)
        {
            _deliveryTypeService = deliveryTypeService;
        }

        [HttpGet]
        [Authorize(Roles = "Client,Employee,Admin")]
        public async Task<ActionResult<IEnumerable<DeliveryTypeResponse>>> GetDeliveryTypes()
        {
            IEnumerable<DeliveryTypeResponse> response = await _deliveryTypeService.GetAllDeliveryTypeResponse();

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult<DeliveryTypeResponse>> CreateDeliveryType([FromForm] DeliveryTypeRequest request)
        {
            DeliveryTypeResponse response = await _deliveryTypeService.CreateNewDeliveryType(request);

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> UpdateProduct(int id, [FromForm] DeliveryTypeRequest request)
        {
            await _deliveryTypeService.UpdateSingleDeliveryTypeById(id, request);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _deliveryTypeService.DeleteSingleDeliveryTypeById(id);

            return Ok();
        }
    }
}