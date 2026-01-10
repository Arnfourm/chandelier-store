using microservices.AnalizeAPI.API.Contracts.Filters;
using microservices.AnalizeAPI.API.Contracts.Responses;
using microservices.AnalizeAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace microservices.AnalizeAPI.API.Controllers
{
    [ApiController]
    [Route("api/Analize/[controller]")]
    [Authorize(Roles = "Employee,Admin")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderAnalysisService _analysisService;

        public OrderController(IOrderAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderAnalysisResponse>>> GetOrderAnalyzeAsync([FromQuery] OrderFilter filters)
        {
            IEnumerable<OrderAnalysisResponse> response = await _analysisService.GetOrderAnalysisResponsesAsync(filters);

            return Ok(response);
        }
    }
}
