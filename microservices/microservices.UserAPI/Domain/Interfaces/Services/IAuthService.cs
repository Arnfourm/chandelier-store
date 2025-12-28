using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace microservices.UserAPI.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> SignUp(UserRequest request);
        Task<AuthResponse> LogIn(LoginRequest request);
        Task<bool> LogOut(Guid userId);

        Task<AuthResponse> RefreshToken(RefreshTokenRequest request);
    }
}
