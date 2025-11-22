using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace microservices.UserAPI.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserDAO _userDAO;
        private readonly IPasswordDAO _passwordDAO;
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;

        public AuthService(IUserDAO userDAO, IPasswordDAO passwordDAO, IUserService userService, IPasswordService passwordService, IJwtService jwtService)
        {
            _userDAO = userDAO;
            _passwordDAO = passwordDAO;
            _userService = userService;
            _passwordService = passwordService;
            _jwtService = jwtService;
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

            var token = _jwtService.GenerateToken(user);

            return new AuthResponse(
                user.GetId(),
                user.GetEmail(),
                token,
                string.Empty
            );
        }

        public async Task<AuthResponse> SignUp(UserRequest request)
        {
            var userId = await _userService.CreateNewUser(request);

            var user = await _userDAO.GetUserById(userId);

            var token = _jwtService.GenerateToken(user);

            return new AuthResponse(
                userId,
                user.GetEmail(),
                token,
                string.Empty
            );
        }

    }
}
