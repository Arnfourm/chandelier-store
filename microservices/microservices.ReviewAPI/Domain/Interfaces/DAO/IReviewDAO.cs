using microservices.ReviewAPI.Domain.Models;

namespace microservices.ReviewAPI.Domain.Interfaces.DAO
{
    public interface IReviewDAO
    {
        Task<List<Review>> GetReviewsAsync();
        Task<Review> GetReviewByIdAsync(Guid id);
        Task<List<Review>> GetReviewByIdsAsync(List<Guid> ids);
        Task<Review> CreateReviewAsync(Review review);
        Task DeleteReview(Guid id);
    }
}
