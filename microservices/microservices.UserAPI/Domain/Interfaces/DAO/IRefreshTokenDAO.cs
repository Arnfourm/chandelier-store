using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Interfaces.DAO
{
    public interface IRefreshTokenDAO
    {
        Task<RefreshToken> GetRefreshTokenById(Guid id);
        Task<RefreshToken> GetRefreshTokenByToken(string token);
        Task<Guid> CreateRefreshToken(RefreshToken refreshToken);
        Task UpdateRefreshToken(RefreshToken refreshToken);
        Task DeleteRefreshToken(Guid? id);
    }
}