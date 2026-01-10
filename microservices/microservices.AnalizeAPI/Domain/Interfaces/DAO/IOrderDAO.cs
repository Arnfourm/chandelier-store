using microservices.AnalizeAPI.Domain.Models;

namespace microservices.AnalizeAPI.Domain.Interfaces.DAO
{
    public interface IOrderDAO
    {
        Task<List<OrderStats>> GetOrdersAsync(DateTime? from, DateTime? to);
    }
}
