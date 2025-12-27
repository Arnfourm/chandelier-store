using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;

using User = microservices.UserAPI.Domain.Models.User;

namespace microservices.UserAPI.Domain.Interfaces.Services
{
    public interface IFavouritesService
    {
        Task<IEnumerable<Guid>> GetFavouriteProductsAsync(Guid userId);
        Task<int> GetFavouritesCountAsync(Guid userId);
        Task AddProductToFavouritesAsync(Guid userId, Guid productId);
        Task RemoveProductFromFavouritesAsync(Guid userId, Guid productId);
        Task<bool> IsProductInFavouritesAsync(Guid userId, Guid productId);
    }
}