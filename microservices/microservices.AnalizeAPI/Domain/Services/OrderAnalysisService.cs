using microservices.AnalizeAPI.API.Contracts.Filters;
using microservices.AnalizeAPI.API.Contracts.Responses;
using microservices.AnalizeAPI.Domain.Interfaces.DAO;
using microservices.AnalizeAPI.Domain.Interfaces.Services;
using microservices.AnalizeAPI.Domain.Models;

namespace microservices.AnalizeAPI.Domain.Services
{
    public class OrderAnalysisService : IOrderAnalysisService
    {
        private readonly IOrderDAO _orderDAO;

        public OrderAnalysisService(IOrderDAO orderDAO)
        {
            _orderDAO = orderDAO;
        }

        public async Task<IEnumerable<OrderAnalysisResponse>> GetOrderAnalysisResponsesAsync(OrderFilter filter)
        {
            List<OrderStats> orderStats = await _orderDAO.GetOrdersAsync(filter.startDate, filter.endDate);

            IEnumerable<OrderAnalysisResponse> response = orderStats
                .Select(orderStat =>
                    new OrderAnalysisResponse
                    (
                        orderStat.Date,
                        orderStat.TotalAmount,
                        orderStat.OrderCount,
                        orderStat.AvgOrderAmount
                    )
                );

            return response;
        }
    }
}
