using microservices.ReviewAPI.Domain.Models;

namespace microservices.ReviewAPI.Domain.Interfaces.DAO
{
    public interface IReviewDAO
    {
        Task<List<Review>> GetReviewsAsync();
        Task<Review> GetReviewByIdAsync(Guid id);
        Task<List<Review>> GetReviewsByUserIdAsync(Guid userId);
        Task<List<Review>> GetReviewsByProductIdAsync(Guid productId);
        Task<Review> CreateReviewAsync(Review review);
        Task<Guid> UpdateReviewAsync(Review review);
        Task DeleteReview(Guid id);
    }
}
