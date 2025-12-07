using microservices.OrderAPI.Domain.Models;

namespace microservices.OrderAPI.Domain.Interfaces.DAO
{
    public interface IOrderDAO
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(Guid id);
        Task<List<Order>> GetOrderByIds(List<Guid> ids);
        Task CreateOrder(Order order);
        Task UpdateOrder(Order order);
        Task DeleteOrder(Guid id);
    }
}
