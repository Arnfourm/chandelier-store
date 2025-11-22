using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;

using User = microservices.UserAPI.Domain.Models.User;

namespace microservices.UserAPI.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllUsers();
        Task<UserResponse> GetSingleUserById(Guid id);
        Task<UserResponse> GetSingleUserByEmail(string email);
        Task<Guid> CreateNewUser(UserRequest request);
        Task UpdateUser(UserRequest request);
        Task DeleteSingleUserById(Guid id);

        //Task<IEnumerable<ClientResponse>> GetAllClients();
        //Task<Client> GetSingleClientByUserId(Guid userId);
        //Task<Guid> CreateNewClient(ClientRequest request);
        //Task UpdateClient(ClientRequest request);
        //Task DeleteSingleClientByUserId(Guid userId);

        //Task<IEnumerable<EmployeeResponse>> GetAllEmployees();
        //Task<Employee> GetSingleEmployeeByUserId(Guid userId);
        //Task<Guid> CreateNewEmployee(EmployeeRequest request);
        //Task UpdateEmployee(EmployeeRequest request);
        //Task DeleteSingleEmployeeByUserId(Guid userId);

        //Task<FavouritesResponse> GetUserFavourites(Guid userId);
        //Task<int> GetUserFavouritesCount(Guid userId);
        //Task AddProductToUserFavourites(Guid userId, Guid productId);
        //Task RemoveProductFromUserFavourites(Guid userId, Guid productId);
        //Task<bool> IsProductInFavourites(Guid userId, Guid productId);
    }
}