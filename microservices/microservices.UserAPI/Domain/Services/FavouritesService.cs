using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Interfaces.Services;
using microservices.UserAPI.Domain.Models;

namespace microservices.UserAPI.Domain.Services
{
    public class FavouritesService : IFavouritesService
    {
        private readonly IFavouritesDAO _favouritesDAO;

        public FavouritesService(IFavouritesDAO favouritesDAO)
        {
            _favouritesDAO = favouritesDAO;
        }

        public async Task<IEnumerable<Guid>> GetFavouriteProductsAsync(Guid userId)
        {
            return await _favouritesDAO.GetFavouriteProductIds(userId);
        }

        public async Task<int> GetFavouritesCountAsync(Guid userId)
        {
            return await _favouritesDAO.GetFavouritesCount(userId);
        }

        public async Task AddProductToFavouritesAsync(Guid userId, Guid productId)
        {
            bool alreadyExists = await _favouritesDAO.IsProductInFavourites(userId, productId);

            if (alreadyExists)
                throw new ArgumentException($"Product with id {productId} is already in favourites for user {userId}");

            await _favouritesDAO.AddProductToFavourites(userId, productId);
        }

        public async Task RemoveProductFromFavouritesAsync(Guid userId, Guid productId)
        {
            await _favouritesDAO.RemoveProductFromFavourites(userId, productId);
        }

        public async Task<bool> IsProductInFavouritesAsync(Guid userId, Guid productId)
        {
            return await _favouritesDAO.IsProductInFavourites(userId, productId);
        }
    }
}
