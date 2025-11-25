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

        public async Task<RefreshToken> GetRefreshTokenByToken(string token)
        {
            var refreshTokenEntity = await _userDbContext.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);

            if (refreshTokenEntity == null)
                return null;

            return new RefreshToken(
                refreshTokenEntity.Id,
                refreshTokenEntity.Token,
                refreshTokenEntity.CreatedTime,
                refreshTokenEntity.ExpireTime
            );
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

        public async Task UpdateRefreshToken(RefreshToken refreshToken)
        {
            await _userDbContext.RefreshTokens
                .Where(rt => rt.Id == refreshToken.GetId())
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(rt => rt.Token, refreshToken.GetToken())
                    .SetProperty(rt => rt.CreatedTime, refreshToken.GetCreatedTime())
                    .SetProperty(rt => rt.ExpireTime, refreshToken.GetExpireTime())
                );

            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to update refresh token: {ex.Message}", ex);
            }
        }

        public async Task DeleteRefreshToken(Guid? id)
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