using microservices.UserAPI.Domain.Interfaces.Services;

namespace microservices.UserAPI.Domain.Services
{
    public class PasswordService : IPasswordService
    {
        public (byte[] Hash, byte[] Salt) HashPassword(string password)
        {
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
