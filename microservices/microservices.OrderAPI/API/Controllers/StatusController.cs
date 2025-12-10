using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace microservices.OrderAPI.API.Controllers
{
    [ApiController]
    [Route("api/Order/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusResponse>>> GetStatuses()
        {
            IEnumerable<StatusResponse> response = await _statusService.GetAllStatusResponses();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<StatusResponse>> CreateStatus(StatusRequest request)
        {
            StatusResponse response = await _statusService.CreateNewStatus(request);

            return Ok(response);
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateStatus(int id, StatusRequest request)
        {
            await _statusService.UpdateStatusById(id, request);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteStatus(int id)
        {
            await _statusService.DeleteStatusById(id);

            return Ok();
        }
    }
}