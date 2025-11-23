using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Interfaces.Services;
using microservices.UserAPI.Domain.Models;
using microservices.UserAPI.Domain.Enums;

using User = microservices.UserAPI.Domain.Models.User;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace microservices.UserAPI.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDAO _userDAO;
        private readonly IPasswordDAO _passwordDAO;
        private readonly IPasswordService _passwordService;

        public UserService(IUserDAO userDAO, IPasswordDAO passwordDAO, IPasswordService passwordService)
        {
            _userDAO = userDAO;
            _passwordDAO = passwordDAO;
            _passwordService = passwordService;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var users = await _userDAO.GetUsers();
            return users.Select(
                user => new UserResponse(
                    Id: user.GetId(),
                    Email: user.GetEmail(),
                    Name: user.GetName(),
                    Surname: user.GetSurname(),
                    Birthday: user.GetBirthday(),
                    Registration: user.GetRegistration(),
                    UserRole: user.GetUserRole()
                )
            );
        }

        public async Task<UserResponse> GetSingleUserById(Guid id)
        {
            var user = await _userDAO.GetUserById(id);
            return new UserResponse (
                Id: user.GetId(),
                Email: user.GetEmail(),
                Name: user.GetName(),
                Surname: user.GetSurname(),
                Birthday: user.GetBirthday(),
                Registration: user.GetRegistration(),
                UserRole: user.GetUserRole()
            );
        }

        public async Task<UserResponse> GetSingleUserByEmail(string email)
        {
            var user = await _userDAO.GetUserByEmail(email);
            return new UserResponse(
                Id: user.GetId(),
                Email: user.GetEmail(),
                Name: user.GetName(),
                Surname: user.GetSurname(),
                Birthday: user.GetBirthday(),
                Registration: user.GetRegistration(),
                UserRole: user.GetUserRole()
            );
        }

        public async Task<Guid> CreateNewUser(UserRequest request)
        {
            var existingUser = await _userDAO.GetUserByEmail(request.Email);
            if (existingUser != null)
                throw new InvalidOperationException($"User with email {request.Email} already exists");
           
            var password = _passwordService.CreatePassword(request.Password);
            var passwordId = await _passwordDAO.CreatePassword(password);

            var user = new User(
                request.Email,
                request.Name,
                request.Surname,
                request.Birthday,
                DateTime.UtcNow,
                passwordId,
                null,
                request.UserRole
            );

            var userId = await _userDAO.CreateUser(user);
            return userId;
        }

        public async Task UpdateUser(UserRequest request)
        {
            var currentUser = await _userDAO.GetUserByEmail(request.Email);

            var updatedUser = new User(
                currentUser.GetId(),
                request.Email ?? currentUser.GetEmail(),
                request.Name ?? currentUser.GetName(),
                request.Surname ?? currentUser.GetSurname(),
                request.Birthday,
                currentUser.GetRegistration(),
                currentUser.GetPasswordId(),
                currentUser.GetRefreshTokenId(),
                request.UserRole
            );

            await _userDAO.UpdateUser(updatedUser);
        }

        public async Task DeleteSingleUserById(Guid id)
        {
            await _userDAO.DeleteUser(id);
        }
    }
}