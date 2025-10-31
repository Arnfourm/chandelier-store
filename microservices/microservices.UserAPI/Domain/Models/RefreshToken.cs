namespace microservices.UserAPI.Domain.Models
{
    public class RefreshToken
    {
        private Guid Id;
        private string Token;
        private DateTime CreatedTime;
        private DateTime ExpireTime;
        
        public RefreshToken(string token, DateTime createdTime, DateTime expireTime)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException("Token can't be empty or with only zeros", nameof(token));
            if (token.Length < 12) throw new ArgumentException("Token lenght can't be less than 12 chars", nameof(token));

            if (expireTime <= createdTime) throw new ArgumentException("Expire date can't be less than created", nameof(expireTime));

            Token = token;
            CreatedTime = createdTime;
            ExpireTime = expireTime;
        }

        public RefreshToken(Guid id, string token, DateTime createdTime, DateTime expireTime)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException("Token can't be empty or with only zeros", nameof(token));
            if (token.Length < 12) throw new ArgumentException("Token lenght can't be less than 12 chars", nameof(token));

            if (expireTime <= createdTime) throw new ArgumentException("Expire date can't be less than created", nameof(expireTime));

            Id = id;
            Token = token;
            CreatedTime = createdTime;
            ExpireTime = expireTime;
        }

        public Guid GetId() { return Id; }
        public string GetToken() { return Token; }
        public DateTime GetCreatedTime() { return CreatedTime; }
        public DateTime GetExpireTime() { return ExpireTime; }

        public void SetToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new ArgumentNullException("Token can't be empty or with only zeros", nameof(token));
            if (token.Length < 12) throw new ArgumentException("Token lenght can't be less than 12 chars", nameof(token));
             
            Token = token;
        }
        public void SetTokenTime(DateTime createdTime, DateTime expireTime)
        {
            if (expireTime <= createdTime) throw new ArgumentException("Expire date can't be less than created", nameof(expireTime));

            CreatedTime = createdTime;
            ExpireTime = expireTime;
        }
    }
}