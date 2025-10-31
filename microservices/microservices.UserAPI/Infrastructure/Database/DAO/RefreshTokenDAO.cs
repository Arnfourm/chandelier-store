namespace microservices.UserAPI.Infrastructure.Database.DAO
{
    public class RefreshTokenDAO : IRefreshTokenDAO
    {
        private readonly UserDbContext _userDbContext;

        public PasswordDAO(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<RefreshToken> GetRefreshTokenById(Guid id)
        {
            var refreshToken = await _userDbContext.RefreshTokens.SingleOrDefaultAsync(refreshToken => refreshToken.Id = id);

            if (refreshToken == null)
            {
                throw new Exception($"Refresh token with id {id} not found");
            }

            return new RefreshToken(refreshToken.Id, refreshToken.RefreshToken, refreshToken.CreatedTime, refreshToken.ExpireTime)
        }

        public async Task<Guid> CreateRefreshToken(RefreshToken refreshToken)
        {
            
        }
    }
}