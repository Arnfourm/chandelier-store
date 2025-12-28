using System.Net.Http.Headers;
using microservices.OrderAPI.Domain.Interfaces.DAO;
using microservices.OrderAPI.Domain.Interfaces.Services;
using microservices.OrderAPI.Domain.Models;
using microservices.OrderAPI.Domain.DTO.Requests;
using microservices.OrderAPI.API.Contracts.Requests;
using Microsoft.Extensions.Configuration;
using microservices.OrderAPI.Domain.DTO.Responses;
using microservices.OrderAPI.API.Contracts.Responses;

namespace microservices.OrderAPI.Domain.Services
{
    public class OrderProductService : IOrderProductService
    {
        private readonly IOrderProductDAO _orderProductDAO;
        private readonly IOrderDAO _orderDAO;
        private readonly ITokenService _tokenService;

        private readonly string _catalogMicroservice;

        public OrderProductService(
            IOrderProductDAO orderProductDAO,
            IOrderDAO orderDAO,
            ITokenService tokenService,
            IConfiguration config)
        {
            _orderProductDAO = orderProductDAO;
            _orderDAO = orderDAO;
            _tokenService = tokenService;

            _catalogMicroservice = config["Microservices:CatalogMicroservice:Url"]
                ?? throw new ArgumentException("Catalog microservice url is null");
        }

        public async Task<IEnumerable<OrderProductResponse>> GetProductsByOrderIdAsync(Guid orderId)
        {
            List<OrderProduct> orderProducts =
                await _orderProductDAO.GetOrderProductByOrderId(orderId);

            IEnumerable<OrderProductResponse> response = orderProducts
                .Select(op => new OrderProductResponse(
                    op.GetOrderId(),
                    op.GetProductId(),
                    op.GetUnitPrice(),
                    op.GetQuantity()
                ));

            return response;
        }

        public async Task AddProductToOrderAsync(OrderProductRequest request)
        {
            string token = await _tokenService.GetTokenAsync();

            decimal unitPrice;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseProduct =
                    await client.GetAsync($"{_catalogMicroservice}/Product/{request.ProductId}");

                if (!responseProduct.IsSuccessStatusCode)
                    throw new Exception("Product doesn't exist");

                ProductResponseDTO product =
                    await responseProduct.Content.ReadFromJsonAsync<ProductResponseDTO>()
                    ?? throw new Exception("Can't parse product");

                unitPrice = product.Price;
            }

            OrderProduct orderProduct = new OrderProduct(
                request.OrderId,
                request.ProductId,
                unitPrice,
                request.Quantity
            );

            await _orderProductDAO.CreateOrderProduct(orderProduct);

            Order order = await _orderDAO.GetOrderById(request.OrderId);

            decimal newTotal =
                order.GetTotalAmount() + unitPrice * request.Quantity;

            Order updatedOrder = new Order(
                order.GetId(),
                order.GetUserId(),
                newTotal,
                order.GetStatusId(),
                order.GetDeliveryTypeId(),
                order.GetCreationDate()
            );

            await _orderDAO.UpdateOrder(updatedOrder);
        }

        public async Task RemoveProductFromOrderAsync(Guid orderId, Guid productId)
        {
            List<OrderProduct> products =
                await _orderProductDAO.GetOrderProductByOrderId(orderId);

            OrderProduct orderProduct = products
                .FirstOrDefault(p => p.GetProductId() == productId)
                ?? throw new Exception("Product not found in order");

            int quantity = orderProduct.GetQuantity();
            decimal unitPrice = orderProduct.GetUnitPrice();

            await _orderProductDAO.DeleteOrderProductByBothIds(orderId, productId);

            Order order = await _orderDAO.GetOrderById(orderId);

            decimal newTotal =
                order.GetTotalAmount() - unitPrice * quantity;

            Order updatedOrder = new Order(
                order.GetId(),
                order.GetUserId(),
                newTotal,
                order.GetStatusId(),
                order.GetDeliveryTypeId(),
                order.GetCreationDate()
            );

            await _orderDAO.UpdateOrder(updatedOrder);
        }
    }
}

