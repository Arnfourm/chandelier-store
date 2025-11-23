using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Interfaces.DAO
{
    public interface IRefreshTokenDAO
    {
        Task<RefreshToken> GetRefreshTokenById(Guid id);
        Task<Guid> CreateRefreshToken(RefreshToken refreshToken);

        //? Task<RefreshToken> UpdateRefreshToken(RefreshToken refreshToken);
        Task DeleteRefreshToken(Guid id);
    }
}