using System.Net;
using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Models;
using microservices.UserAPI.Infrastructure.Database.Contexts;
using microservices.UserAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.UserAPI.Infrastructure.Database.DAO
{
    public class FavouritesDAO : IFavouritesDAO
    {
        private readonly UserDbContext _userDbContext;

        public FavouritesDAO(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<List<Guid>> GetFavouriteProductIds(Guid userId)
        {
            return await _userDbContext.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.ProductId)
                .ToListAsync();
        }

        public async Task<int> GetFavouritesCount(Guid userId)
        {
            return await _userDbContext.Favorites
                .CountAsync(f => f.UserId == userId);
        }

        public async Task AddProductToFavourites(Guid userId, Guid productId)
        {
            var existingFavorite = await _userDbContext.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);

            if (existingFavorite != null)
            {
                throw new Exception($"Product {productId} is already in favorites for user {userId}");
            }

            var favoriteEntity = new FavoritesEntity
            {
                UserId = userId,
                ProductId = productId
            };

            await _userDbContext.Favorites.AddAsync(favoriteEntity);

            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add product to favorites: {ex.Message}", ex);
            }
        }

        public async Task RemoveProductFromFavourites(Guid userId, Guid productId)
        {
            var result = await _userDbContext.Favorites
                .Where(f => f.UserId == userId && f.ProductId == productId)
                .ExecuteDeleteAsync();

            if (result == 0)
            {
                throw new Exception($"Product {productId} not found in favorites for user {userId}");
            }

            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to remove product from favorites: {ex.Message}", ex);
            }
        }

        public async Task<bool> IsProductInFavourites(Guid userId, Guid productId)
        {
            return await _userDbContext.Favorites
                .AnyAsync(f => f.UserId == userId && f.ProductId == productId);
        }
    }
}
