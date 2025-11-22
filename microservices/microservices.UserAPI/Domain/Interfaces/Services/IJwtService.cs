using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        Guid? ValidateToken(string token);
    }
}
