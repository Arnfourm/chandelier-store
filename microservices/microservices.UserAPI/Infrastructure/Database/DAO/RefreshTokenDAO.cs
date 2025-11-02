using microservices.UserAPI.Domain.Models;
using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Infrastructure.Database.Contexts;
using microservices.UserAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.UserAPI.Infrastructure.Database.DAO
{
    public class RefreshTokenDAO : IRefreshTokenDAO
    {
        private readonly UserDbContext _userDbContext;

        public RefreshTokenDAO(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<RefreshToken> GetRefreshTokenById(Guid id)
        {
            var refreshTokenEntity = await _userDbContext.RefreshTokens.SingleOrDefaultAsync(refreshToken => refreshToken.Id == id);

            if (refreshTokenEntity == null)
            {
                throw new Exception($"Refresh token with id {id} not found");
            }

            return new RefreshToken(refreshTokenEntity.Id, refreshTokenEntity.Token, refreshTokenEntity.CreatedTime, refreshTokenEntity.ExpireTime);
        }

        public async Task<Guid> CreateRefreshToken(RefreshToken refreshToken)
        {
            var refreshTokenEntity = new RefreshTokenEntity
            {
                Token = refreshToken.GetToken(),
                CreatedTime = refreshToken.GetCreatedTime(),
                ExpireTime = refreshToken.GetExpireTime()
            };

            await _userDbContext.RefreshTokens.AddAsync(refreshTokenEntity);
            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new refresh token {ex.Message}", ex);
            }

            return refreshTokenEntity.Id;
        }

        public async Task DeleteRefreshToken(Guid id)
        {
            await _userDbContext.RefreshTokens.Where(refreshToken => refreshToken.Id == id).ExecuteDeleteAsync();

            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete refresh token {ex.Message}", ex);
            }
        }
    }
}