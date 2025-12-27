using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace microservices.OrderAPI.API.Controllers
{
    [ApiController]
    [Route("api/OrderProduct")]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductService _orderProductService;

        public OrderProductController(IOrderProductService orderProductService)
        {
            _orderProductService = orderProductService;
        }

        [HttpGet("{orderId:guid}")]
        [Authorize(Roles = "Client,Employee,Admin")]
        public async Task<ActionResult<IEnumerable<OrderProductResponse>>> GetProductsByOrder(Guid orderId)
        {
            IEnumerable<OrderProductResponse> response =
                await _orderProductService.GetProductsByOrderIdAsync(orderId);

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Client,Employee,Admin")]
        public async Task<IActionResult> AddProductToOrder([FromBody] OrderProductRequest request)
        {
            await _orderProductService.AddProductToOrderAsync(request);
            return Ok();
        }

        [HttpDelete("{orderId:guid}/{productId:guid}")]
        [Authorize(Roles = "Client,Employee,Admin")]
        public async Task<IActionResult> RemoveProductFromOrder(Guid orderId, Guid productId)
        {
            await _orderProductService.RemoveProductFromOrderAsync(orderId, productId);
            return Ok();
        }
    }
}
