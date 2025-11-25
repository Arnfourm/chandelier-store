using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Interfaces.DAO
{
    public interface IUserDAO
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByEmail(string email);
        Task<Guid> CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(Guid id);
    }
}