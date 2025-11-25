using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        Guid? ValidateToken(string token);
        (Guid userId, string email)? GetUserInfoFromToken(string token);
    }
}
