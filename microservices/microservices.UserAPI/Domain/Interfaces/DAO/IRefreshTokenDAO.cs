using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Interfaces.DAO
{
    public interface IRefreshTokenDAO
    {
        public Task<RefreshToken> GetRefreshTokenById(Guid id);
        public Task<Guid> CreateRefreshToken(RefreshToken refreshToken);
        public Task DeleteRefreshToken(Guid id);
    }
}