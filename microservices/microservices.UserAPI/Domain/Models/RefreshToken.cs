namespace microservices.Domain.Models;
{
    public class RefreshToken
    {
        private Guid Id;
        private string RefreshToken;
        private DateTime CreatedTime;
        private DataTime ExpireTime;
        
        public RefreshToken(string refreshToken)
        {
            // Validation
            RefreshToken = refreshToken;
        }

        public RefreshToken(Guid id, string refreshToken, DateTime createdTime, DateTime expireTime)
        {
            Id = id;

            // Validation
            RefreshToken = refreshToken;
            CreatedTime = createdTime;
            ExpireTime = expireTime;
        }

        public Guid GetId() { return Id; }
        public string GetRefreshToken() { return RefreshToken; }
        public DateTime GetCreatedTime() { return CreatedTime; }
        public DateTime GetExpireTime() { return ExpireTime; }

        public void SetRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
        public void SetCreatedTime(DateTime createdTime)
        {
            CreatedTime = createdTime;
        }
        public void SetExpireTime(DateTime expireTime)
        {
            ExpireTime = expireTime;
        }
    }
}