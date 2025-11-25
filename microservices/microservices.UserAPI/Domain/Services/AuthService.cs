using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Interfaces.Services;
using microservices.UserAPI.Domain.Models;
using Microsoft.AspNetCore.Identity;

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

        public AuthService(IUserDAO userDAO, IPasswordDAO passwordDAO, IRefreshTokenDAO refreshTokenDAO,
            IUserService userService, IPasswordService passwordService, IJwtService jwtService)
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
            var userId = await _userService.CreateNewUser(request);
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

        public async Task<AuthResponse> LogIn(LoginRequest request)
        {
            var user = await _userDAO.GetUserByEmail(request.Email);
            var password = await _passwordDAO.GetPasswordById(user.GetPasswordId());

            var isValid = _passwordService.VerifyPassword(
                request.Password,
                password.GetPasswordHash(),
                password.GetPasswordSaulHash()
            );

            if (!isValid)
                throw new UnauthorizedAccessException("Invalid credentials");

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

        public async Task<bool> LogOut(Guid userId)
        {
            try
            {
                var user = await _userDAO.GetUserById(userId);

                await _refreshTokenDAO.DeleteRefreshToken(user.GetRefreshTokenId());

                var updatedUser = new User(
                        user.GetId(),
                        user.GetEmail(),
                        user.GetName(),
                        user.GetSurname(),
                        user.GetBirthday(),
                        user.GetRegistration(),
                        user.GetPasswordId(),
                        Guid.Empty,
                        user.GetUserRole()
                    );

                await _userDAO.UpdateUser(updatedUser);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<AuthResponse> RefreshToken(RefreshTokenRequest request)
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

            return new AuthResponse(
                user.GetId(),
                user.GetEmail(),
                user.GetUserRole(),
                newAccessToken,
                newRefreshToken
            );
        }

    }
}
