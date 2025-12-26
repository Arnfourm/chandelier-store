using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Interfaces.Services;
using microservices.UserAPI.Domain.Models;

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
            try
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
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve users: {ex.Message}", ex);
            }
        }

        public async Task<UserResponse> GetSingleUserByIdAsync(Guid id)
        {
            try
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
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve user by ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<UserResponse> GetSingleUserByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("Email cannot be empty");

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
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve user by email {email}: {ex.Message}", ex);
            }
        }

        public async Task<Guid> CreateUserAsync(UserRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentException("Request cannot be null");

                if (string.IsNullOrWhiteSpace(request.Email))
                    throw new ArgumentException("Email is required");

                if (string.IsNullOrWhiteSpace(request.Password))
                    throw new ArgumentException("Password is required");

                try
                {
                    var emailAddress = new System.Net.Mail.MailAddress(request.Email);
                }
                catch
                {
                    throw new ArgumentException("Invalid email format");
                }

                if (request.Password.Length < 6)
                    throw new ArgumentException("Password must be at least 6 characters long");

                try
                {
                    var existingUser = await _userDAO.GetUserByEmail(request.Email);
                    throw new ArgumentException($"User with email '{request.Email}' already exists");
                }
                catch (ArgumentException ex) when (ex.Message.Contains("not found", StringComparison.OrdinalIgnoreCase))
                {
                }

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
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create user: {ex.Message}", ex);
            }
        }

        public async Task UpdateUserAsync(UserRequest request)
        {
            try
            {
                if (request == null)
                    throw new ArgumentException("Request cannot be null");

                if (string.IsNullOrWhiteSpace(request.Email))
                    throw new ArgumentException("Email is required");

                try
                {
                    var emailAddress = new System.Net.Mail.MailAddress(request.Email);
                }
                catch
                {
                    throw new ArgumentException("Invalid email format");
                }

                var currentUser = await _userDAO.GetUserByEmail(request.Email);

                if (!string.IsNullOrWhiteSpace(request.Password))
                {
                    if (request.Password.Length < 6)
                        throw new ArgumentException("Password must be at least 6 characters long");

                    var newPassword = _passwordService.CreatePassword(request.Password);
                    var updatedPassword = new Password(
                        currentUser.GetPasswordId() ?? Guid.Empty,
                        newPassword.GetPasswordHash(),
                        newPassword.GetPasswordSaulHash()
                    );
                    await _passwordDAO.UpdatePassword(updatedPassword);
                }

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
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update user: {ex.Message}", ex);
            }
        }

        public async Task DeleteUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _userDAO.GetUserById(id);

                if (user.GetPasswordId().HasValue)
                {
                    await _passwordDAO.DeletePassword(user.GetPasswordId().Value);
                }

                await _userDAO.DeleteUser(id);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete user with ID {id}: {ex.Message}", ex);
            }
        }
    }
}