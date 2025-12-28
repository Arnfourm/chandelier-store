using microservices.ReviewAPI.API.Contracts.Responses;
using microservices.ReviewAPI.API.Contracts.Requests;
using microservices.ReviewAPI.Domain.Models;

namespace microservices.ReviewAPI.Domain.Interfaces.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponse>> GetAllReviewsAsync();
        Task<ReviewResponse> GetReviewByIdAsync(Guid id);
        Task<IEnumerable<ReviewResponse>> GetReviewsByProductIdAsync(Guid productId);
        Task<IEnumerable<ReviewResponse>> GetReviewsByUserIdAsync(Guid userId);
        Task<ReviewResponse> CreateNewReviewAsync(ReviewRequest request);
        Task<Guid> DeleteSingleReviewByIdAsync(Guid id);
    }
}
