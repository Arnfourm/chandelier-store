namespace microservices.UserAPI.Domain.Interfaces.DAO
{
    public interface IFavouritesDAO
    {
        Task<List<Guid>> GetFavouriteProductIds(Guid userId);
        Task<int> GetFavouritesCount(Guid userId);
        Task AddProductToFavourites(Guid userId, Guid productId);
        Task RemoveProductFromFavourites(Guid userId, Guid productId);
        Task<bool> IsProductInFavourites(Guid userId, Guid productId);
    }
}