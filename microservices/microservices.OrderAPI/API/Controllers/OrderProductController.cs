using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.Interfaces.Services;
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
        public async Task<ActionResult<IEnumerable<OrderProductResponse>>> GetProductsByOrder(Guid orderId)
        {
            IEnumerable<OrderProductResponse> response =
                await _orderProductService.GetProductsByOrderIdAsync(orderId);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToOrder([FromBody] OrderProductRequest request)
        {
            await _orderProductService.AddProductToOrderAsync(request);
            return Ok();
        }

        [HttpDelete("{orderId:guid}/{productId:guid}")]
        public async Task<IActionResult> RemoveProductFromOrder(Guid orderId, Guid productId)
        {
            await _orderProductService.RemoveProductFromOrderAsync(orderId, productId);
            return Ok();
        }
    }
}
