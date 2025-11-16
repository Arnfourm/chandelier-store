using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Interfaces.Services;
using microservices.UserAPI.Domain.Models;

using User = microservices.UserAPI.Domain.Models.User;

namespace microservices.UserAPI.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDAO _userDAO;

        public UserService(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        //public async Task<IEnumerable<UserResponse>> GetAllUsers() { }

        public async Task<User> GetSingleUserById(Guid id)
        {
            User user = await _userDAO.GetUserById(id);

            return user;
        }

        public async Task CreateNewUser(UserRequest request)
        {
            

            User newUser = new User(
                request.Title,
                attributeGroup.GetId(),
                measurementUnit.GetId()
            );
            await _attributeDAO.CreateAttribute(newAttribute);
        }

        public async Task UpdateUser(Guid id, UserRequest request) { }

        public async Task DeleteSingleUserById(Guid id) { }
    }
}