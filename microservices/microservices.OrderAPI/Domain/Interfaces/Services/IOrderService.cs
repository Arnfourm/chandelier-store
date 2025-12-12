using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.Models;

namespace microservices.OrderAPI.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetAllOrderResponseAsync();
        Task<Order> GetOrderByIdAsync(Guid Id);
        Task<OrderResponse> CreateNewOrderAsync(OrderRequest request);
        Task UpdateSingleOrderByIdAsync(Guid id, OrderRequest request);
        Task DeleteSingleOrderByIdAsync(Guid id);
    }
}