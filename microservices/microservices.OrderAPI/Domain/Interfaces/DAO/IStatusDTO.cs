using microservices.OrderAPI.Domain.Models;

namespace microservices.OrderAPI.Domain.Interfaces.DAO
{
    public interface IStatusDTO
    {
        Task<List<Status>> GetStatuses();
        Task<Status> GetStatusById(int id);
        Task<List<Status>> GetStatusByIds(List<int> ids);
        Task CreateStatus(Status status);
        Task UpdateStatus(Status status);
        Task DeleteStatus(int id);
    }
}
