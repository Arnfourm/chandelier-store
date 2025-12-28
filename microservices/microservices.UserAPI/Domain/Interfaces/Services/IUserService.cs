using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;

using User = microservices.UserAPI.Domain.Models.User;

namespace microservices.UserAPI.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<UserResponse> GetSingleUserByIdAsync(Guid id);
        Task<UserResponse> GetSingleUserByEmailAsync(string email);
        Task<Guid> CreateUserAsync(UserRequest request);
        Task UpdateUserAsync(UserRequest request);
        Task DeleteUserByIdAsync(Guid id);
    }
}