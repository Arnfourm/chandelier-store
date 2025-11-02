using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Interfaces.DAO
{
    public interface IPasswordDAO
    {
        public Task<Password> GetPasswordById(Guid id);
        public Task<Guid> CreatePassword(Password password);
        public Task DeletePassword(Guid id);
        //public void UpdatePassword(Guid id, Password password);
    }
}