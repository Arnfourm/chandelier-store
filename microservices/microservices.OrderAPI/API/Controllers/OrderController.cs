using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.Interfaces.Services;
using microservices.OrderAPI.Domain.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersAsync()
        {
            IEnumerable<OrderResponse> response = await _orderService.GetAllOrderResponseAsync();

            return Ok(response);
        }   

        [HttpPost]
        [Authorize(Roles = "Client,Employee,Admin")]
        public async Task<ActionResult<OrderResponse>> CreateOrderAsync([FromForm] OrderRequest orderRequest)
        {
            OrderResponse orderResponse = await _orderService.CreateNewOrderAsync(orderRequest);

            return Ok(orderResponse);
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> UpdateOrderAsync(Guid id, [FromBody] OrderRequest orderRequest)
        {
            await _orderService.UpdateSingleOrderByIdAsync(id, orderRequest);

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> DeleteOrderAsync(Guid id)
        {
            await _orderService.DeleteSingleOrderByIdAsync(id);

            return Ok();
        }
    }
}