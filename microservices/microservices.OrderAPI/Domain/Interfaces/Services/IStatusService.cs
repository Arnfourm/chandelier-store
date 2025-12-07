using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.Models;

namespace microservices.OrderAPI.Domain.Interfaces.Services
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusResponse>> GetAllStatusResponses();
        Task<Status> GetStatusById(int id);
        Task<IEnumerable<StatusResponse>> GetStatusResponsesByIds(List<int> ids);
        Task<StatusResponse> CreateNewStatus(StatusRequest request);
        Task UpdateStatusById(int id, StatusRequest request);
        Task DeleteStatusById(int id);
    }
}
