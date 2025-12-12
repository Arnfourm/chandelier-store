using microservices.OrderAPI.Domain.Interfaces.DAO;
using microservices.OrderAPI.Domain.Interfaces.Services;

namespace microservice.SupplyAPI.Domain.Services
{
    public class OrderProductService : IOrderProductService
    {
        private readonly IOrderProductDAO _orderProductDAO;

        public OrderProductService(IOrderProductDAO orderProductDAO)
        {
            _orderProductDAO = orderProductDAO;
        }

        
    }
}