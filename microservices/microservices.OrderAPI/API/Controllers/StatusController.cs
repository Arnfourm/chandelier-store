using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Client,Employee,Admin")]
        public async Task<ActionResult<IEnumerable<StatusResponse>>> GetStatuses()
        {
            IEnumerable<StatusResponse> response = await _statusService.GetAllStatusResponses();

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult<StatusResponse>> CreateStatus(StatusRequest request)
        {
            StatusResponse response = await _statusService.CreateNewStatus(request);

            return Ok(response);
        }
        
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> UpdateStatus(int id, StatusRequest request)
        {
            await _statusService.UpdateStatusById(id, request);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> DeleteStatus(int id)
        {
            await _statusService.DeleteStatusById(id);

            return Ok();
        }
    }
}