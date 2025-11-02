using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Interfaces.DAO
{
    public interface IUserDAO
    {
        public Task<List<User>> GetUsers();
        public Task<User> GetUserById(Guid id);
        public Task<User> GetUserByEmail(string email);
        public Task<Guid> CreateUser(User user); 
        public Task DeleteUser(Guid id);
    }
}