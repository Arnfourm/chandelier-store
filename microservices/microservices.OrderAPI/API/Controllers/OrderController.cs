using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace microservices.OrderAPI.API.Controllers
{
    [ApiController]
    [Route("api/Order/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrderResponsesAsync()
        {
            IEnumerable<OrderResponse> response = await _orderService.GetAllOrderResponseAsync();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponse>> CreateOrderAsync([FromForm] OrderRequest orderRequest)
        {
            OrderResponse orderResponse = await _orderService.CreateNewOrderAsync(orderRequest);

            return orderResponse;
        }
    }
}