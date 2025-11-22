using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace microservices.UserAPI.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> LogIn(LoginRequest request);
        //Task LogOut(AuthRequest request);
        Task<AuthResponse> SignUp(UserRequest request);
    }
}
