using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;

namespace microservices.OrderAPI.Domain.Interfaces.Services
{
    public interface IOrderProductService
    {
        Task<IEnumerable<OrderProductResponse>> GetProductsByOrderIdAsync(Guid orderId);
        Task AddProductToOrderAsync(OrderProductRequest request);
        Task RemoveProductFromOrderAsync(Guid orderId, Guid productId);
        
    }
}