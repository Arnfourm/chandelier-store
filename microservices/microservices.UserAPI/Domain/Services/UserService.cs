using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Interfaces.Services;
using microservices.UserAPI.Domain.Models;
using microservices.UserAPI.Domain.Enums;

using User = microservices.UserAPI.Domain.Models.User;

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

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
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

        public async Task<UserResponse> GetSingleUserByIdAsync(Guid id)
        {
            var user = await _userDAO.GetUserById(id);
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

        public async Task<UserResponse> GetSingleUserByEmailAsync(string email)
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

        public async Task<Guid> CreateUserAsync(UserRequest request)
        {
            User existingUser = await _userDAO.GetUserByEmail(request.Email);
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

        public async Task UpdateUserAsync(UserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentException("Email cannot be null or empty");

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name cannot be null or empty");

            if (string.IsNullOrWhiteSpace(request.Surname))
                throw new ArgumentException("Surname cannot be null or empty");

            if (request.Birthday.HasValue && request.Birthday.Value > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new ArgumentException("Birthday cannot be in the future");

            var currentUser = await _userDAO.GetUserByEmail(request.Email);
            if (currentUser == null)
                throw new KeyNotFoundException($"User with email '{request.Email}' not found");

            var updatedUser = new User(
                currentUser.GetId(),
                request.Email ?? currentUser.GetEmail(),
                request.Name ?? currentUser.GetName(),
                request.Surname ?? currentUser.GetSurname(),
                request.Birthday ?? currentUser.GetBirthday(),
                currentUser.GetRegistration(),
                currentUser.GetPasswordId(),
                currentUser.GetRefreshTokenId(),
                request.UserRole
            );

            await _userDAO.UpdateUser(updatedUser);
        }

        public async Task DeleteUserByIdAsync(Guid id)
        {
            await _userDAO.DeleteUser(id);
        }
    }
}