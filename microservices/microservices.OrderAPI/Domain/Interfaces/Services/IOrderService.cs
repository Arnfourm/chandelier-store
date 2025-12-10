using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.Models;

namespace microservices.OrderAPI.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetAllOrderResponse();
        Task<Order> GetOrderById(Guid Id);
    }
}