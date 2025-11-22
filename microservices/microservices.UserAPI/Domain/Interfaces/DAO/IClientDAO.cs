using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Interfaces.DAO
{
    public interface IClientDAO
    {
        public Task<List<Client>> GetClients();
        Task<Client> GetClientByUserId(Guid userId);
        Task<Guid> CreateClient(Client client);
        Task UpdateClient(Client client);
        Task DeleteClient(Guid userId);
    }
}
