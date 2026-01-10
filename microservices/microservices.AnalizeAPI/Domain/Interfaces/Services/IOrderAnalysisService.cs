using microservices.AnalizeAPI.API.Contracts.Filters;
using microservices.AnalizeAPI.API.Contracts.Responses;

namespace microservices.AnalizeAPI.Domain.Interfaces.Services
{
    public interface IOrderAnalysisService
    {
        Task<IEnumerable<OrderAnalysisResponse>> GetOrderAnalysisResponsesAsync(OrderFilter filter);
    }
}
