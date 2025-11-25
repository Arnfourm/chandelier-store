using microservices.UserAPI.Domain.Interfaces.Services;
using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Services
{
    public class PasswordService : IPasswordService
    {
        public Password CreatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            var (hash, salt) = HashPassword(password);
            return new Password(hash, salt);
        }

        public (byte[] Hash, byte[] Salt) HashPassword(string password)
        {
            // Заглушка
            var hash = System.Text.Encoding.UTF8.GetBytes($"hash_{password}");
            var salt = System.Text.Encoding.UTF8.GetBytes("static_salt");

            return (hash, salt);
        }

        public bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            var hash = System.Text.Encoding.UTF8.GetBytes($"hash_{password}");
            return storedHash.SequenceEqual(hash);
        }
    }
}
