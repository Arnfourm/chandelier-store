using microservices.OrderAPI.Domain.Models;

namespace microservices.OrderAPI.Domain.Interfaces.DAO
{
    public interface IOrderProductDAO
    {
        Task<List<OrderProduct>> GetOrderProductByOrderId(Guid orderId);
        Task<List<OrderProduct>> GetOrderProductByProductId(Guid productId);
        Task CreateOrderProduct(OrderProduct orderProduct);
        Task DeleteOrderProductByOrderId(Guid orderId);
        Task DeleteOrderProductByProductId(Guid productId);
        Task DeleteOrderProductByBothIds(Guid orderId, Guid productId);
    }
}
