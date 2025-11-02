namespace microservices.UserAPI.Domain.Models
{
    public class Password
    {
        private Guid Id;
        private byte[] PasswordHash;
        private byte[] PasswordSaulHash;

        public Password(byte[] passwordHash, byte[] passwordSaulHash)
        {
            if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));
            if (passwordSaulHash == null) throw new ArgumentNullException(nameof(passwordSaulHash));

            if (passwordHash.Length == 0) throw new ArgumentException("Password hash can't be empty", nameof(passwordHash));
            if (passwordSaulHash.Length == 0) throw new ArgumentException("PasswordSaul hash can't be empty", nameof(passwordSaulHash));

            PasswordHash = passwordHash;
            PasswordSaulHash = passwordSaulHash;
        }

        public Password(Guid id, byte[] passwordHash, byte[] passwordSaulHash)
        {
            if (passwordHash.Length == 0) throw new ArgumentException("Password hash can't be empty", nameof(passwordHash));
            if (passwordSaulHash.Length == 0) throw new ArgumentException("PasswordSaul hash can't be empty", nameof(passwordSaulHash));

            Id = id;
            PasswordHash = passwordHash;
            PasswordSaulHash = passwordSaulHash;
        }

        public Guid GetId() { return Id; }
        public byte[] GetPasswordHash() { return PasswordHash; }
        public byte[] GetPasswordSaulHash() { return PasswordSaulHash; }

        public void SetPasswordHash(byte[] passwordHash) {
            if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));
            if (passwordHash.Length == 0) throw new ArgumentException("Password hash can't be empty", nameof(passwordHash));

            PasswordHash = passwordHash;
        }
        public void SetPasswordSaulHash(byte[] passwordSaulHash)
        {
            if (passwordSaulHash == null) throw new ArgumentNullException(nameof(passwordSaulHash));
            if (passwordSaulHash.Length == 0) throw new ArgumentException("PasswordSaul hash can't be empty", nameof(passwordSaulHash));

            PasswordSaulHash = passwordSaulHash;
        }
    }
}