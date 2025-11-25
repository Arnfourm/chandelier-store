using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Interfaces.Services
{
    public interface IPasswordService
    {
        Password CreatePassword(string password);
        (byte[] Hash, byte[] Salt) HashPassword(string password);
        bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt);
    }
}
