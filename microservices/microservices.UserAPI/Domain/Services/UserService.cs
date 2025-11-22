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
            ValidateCreateRequest(request);

            try
            {
                var existingUser = await _userDAO.GetUserByEmail(request.Email);
                if (existingUser != null)
                    throw new InvalidOperationException($"User with email {request.Email} already exists");
            }
            catch (Exception ex) when (ex.Message.Contains("not found"))
            { }

            var (passwordHash, passwordSalt) = _passwordService.HashPassword(request.Password);
            var password = new Password(passwordHash, passwordSalt);
            var passwordId = await _passwordDAO.CreatePassword(password);

            var user = new User(
                request.Email,
                request.Name,
                request.Surname,
                request.Birthday,
                DateTime.UtcNow,
                passwordId,
                Guid.Empty,
                request.UserRole
            );

            var userId = await _userDAO.CreateUser(user);
            return userId;
        }

        public async Task UpdateUser(UserRequest request)
        {
            if (request.Id == null)
                throw new ArgumentException("Id is required for update user");

            var currentUser = await _userDAO.GetUserById(request.Id.Value);

            var updatedUser = new User(
                request.Id.Value,
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

        private async void ValidateCreateRequest(UserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentException("Email is required");

            if (request.Id != null)
                throw new ArgumentException("Id must be null for create operation");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Password is required for user creation");            

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name is required");

            if (string.IsNullOrWhiteSpace(request.Surname))
                throw new ArgumentException("Surname is required");
        }
    }
}