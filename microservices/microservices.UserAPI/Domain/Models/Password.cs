namespace microservices.UserAPI.Domain.Models;
{
    public class Password
    {
        private Guid Id;
        private byte[] PasswordHash;
        private byte[] PasswordSaulHash;

        public Password(byte[] passwordHash, byte[] passwordSaulHash)
        {
            // Add validation
            PasswordHash = passwordHash;
            PasswordSaulHash = passwordSaulHash;
        }

        public Password(Guid id, byte[] passwordHash, byte[] passwordSaulHash)
        {
            Id = id;

            // Add validation
            PasswordHash = passwordHash;
            PasswordSaulHash = passwordSaulHash;
        }

        public Guid GetId() { return Id; }
        public byte[] GetPasswordHash() { return PasswordHash; }
        public byte[] GetPasswordSaulHash() { return PasswordSaulHash; }
    }
}