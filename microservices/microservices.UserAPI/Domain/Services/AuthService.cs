using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Interfaces.Services;
using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserDAO _userDAO;
        private readonly IPasswordDAO _passwordDAO;
        private readonly IRefreshTokenDAO _refreshTokenDAO;
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;

        public AuthService(
            IUserDAO userDAO,
            IPasswordDAO passwordDAO,
            IRefreshTokenDAO refreshTokenDAO,
            IUserService userService,
            IPasswordService passwordService,
            IJwtService jwtService)
        {
            _userDAO = userDAO;
            _passwordDAO = passwordDAO;
            _refreshTokenDAO = refreshTokenDAO;
            _userService = userService;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> SignUp(UserRequest request)
        {
            try
            {
                var existingUsers = await _userDAO.GetUsers();
                var existingUser = existingUsers.FirstOrDefault(u =>
                    string.Equals(u.GetEmail(), request.Email, StringComparison.OrdinalIgnoreCase));

                if (existingUser != null)
                {
                    throw new ArgumentException($"User with email '{request.Email}' already exists");
                }

                var userId = await _userService.CreateUserAsync(request);
                var user = await _userDAO.GetUserById(userId);

                var accessToken = _jwtService.GenerateAccessToken(user);
                var refreshToken = _jwtService.GenerateRefreshToken();

                var refreshTokenEntity = new RefreshToken(
                    refreshToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddDays(7)
                );

                var refreshTokenId = await _refreshTokenDAO.CreateRefreshToken(refreshTokenEntity);

                var updatedUser = new User(
                    user.GetId(),
                    user.GetEmail(),
                    user.GetName(),
                    user.GetSurname(),
                    user.GetBirthday(),
                    user.GetRegistration(),
                    user.GetPasswordId(),
                    refreshTokenId,
                    user.GetUserRole()
                );
                await _userDAO.UpdateUser(updatedUser);

                return new AuthResponse(
                    userId,
                    user.GetEmail(),
                    user.GetUserRole(),
                    accessToken,
                    refreshToken
                );
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Registration failed: {ex.Message}", ex);
            }
        }

        public async Task<AuthResponse> LogIn(LoginRequest request)
        {
            try
            {
                var allUsers = await _userDAO.GetUsers();
                var user = allUsers.FirstOrDefault(u =>
                    string.Equals(u.GetEmail(), request.Email, StringComparison.OrdinalIgnoreCase));

                if (user == null)
                {
                    throw new ArgumentException($"User with email '{request.Email}' not found");
                }

                var password = await _passwordDAO.GetPasswordById(user.GetPasswordId());

                var isValid = _passwordService.VerifyPassword(
                    request.Password,
                    password.GetPasswordHash(),
                    password.GetPasswordSaulHash()
                );

                if (!isValid)
                    throw new UnauthorizedAccessException("Invalid password");

                var accessToken = _jwtService.GenerateAccessToken(user);
                var refreshToken = _jwtService.GenerateRefreshToken();

                var refreshTokenEntity = new RefreshToken(
                    refreshToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddDays(7)
                );
                var refreshTokenId = await _refreshTokenDAO.CreateRefreshToken(refreshTokenEntity);

                var updatedUser = new User(
                    user.GetId(),
                    user.GetEmail(),
                    user.GetName(),
                    user.GetSurname(),
                    user.GetBirthday(),
                    user.GetRegistration(),
                    user.GetPasswordId(),
                    refreshTokenId,
                    user.GetUserRole()
                );
                await _userDAO.UpdateUser(updatedUser);

                return new AuthResponse(
                    user.GetId(),
                    user.GetEmail(),
                    user.GetUserRole(),
                    accessToken,
                    refreshToken
                );
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Login failed: {ex.Message}", ex);
            }
        }

        public async Task<bool> LogOut(Guid userId)
        {
            try
            {
                var user = await _userDAO.GetUserById(userId);

                var updatedUser = new User(
                    user.GetId(),
                    user.GetEmail(),
                    user.GetName(),
                    user.GetSurname(),
                    user.GetBirthday(),
                    user.GetRegistration(),
                    user.GetPasswordId(),
                    null,
                    user.GetUserRole()
                );
                await _userDAO.UpdateUser(updatedUser);

                if (user.GetRefreshTokenId().HasValue && user.GetRefreshTokenId().Value != Guid.Empty)
                {
                    await _refreshTokenDAO.DeleteRefreshToken(user.GetRefreshTokenId().Value);
                }

                return true;
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"User with id {userId} not found", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Logout error: {ex.Message}");
            }
        }

        public async Task<AuthResponse> RefreshToken(RefreshTokenRequest request)
        {
            try
            {
                var storedRefreshToken = await _refreshTokenDAO.GetRefreshTokenByToken(request.RefreshToken);

                if (storedRefreshToken == null)
                    throw new UnauthorizedAccessException("Invalid refresh token");

                if (storedRefreshToken.GetExpireTime() < DateTime.UtcNow)
                    throw new UnauthorizedAccessException("Refresh token expired");

                var allUsers = await _userDAO.GetUsers();
                var user = allUsers.FirstOrDefault(u => u.GetRefreshTokenId() == storedRefreshToken.GetId());

                if (user == null)
                    throw new UnauthorizedAccessException("User not found for this refresh token");

                var newAccessToken = _jwtService.GenerateAccessToken(user);
                var newRefreshToken = _jwtService.GenerateRefreshToken();

                var updatedRefreshToken = new RefreshToken(
                    storedRefreshToken.GetId(),
                    newRefreshToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddDays(7)
                );
                await _refreshTokenDAO.UpdateRefreshToken(updatedRefreshToken);

                var updatedUser = new User(
                    user.GetId(),
                    user.GetEmail(),
                    user.GetName(),
                    user.GetSurname(),
                    user.GetBirthday(),
                    user.GetRegistration(),
                    user.GetPasswordId(),
                    storedRefreshToken.GetId(),
                    user.GetUserRole()
                );
                await _userDAO.UpdateUser(updatedUser);

                return new AuthResponse(
                    user.GetId(),
                    user.GetEmail(),
                    user.GetUserRole(),
                    newAccessToken,
                    newRefreshToken
                );
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Token refresh failed: {ex.Message}", ex);
            }
        }
    }
}