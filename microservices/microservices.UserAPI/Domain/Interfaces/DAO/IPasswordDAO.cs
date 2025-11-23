using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Interfaces.DAO
{
    public interface IPasswordDAO
    {
        Task<Password> GetPasswordById(Guid? id);
        Task<Guid> CreatePassword(Password password);
        Task UpdatePassword(Password password);
        Task DeletePassword(Guid id);
    }
}