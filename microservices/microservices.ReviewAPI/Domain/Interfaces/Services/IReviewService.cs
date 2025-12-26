using microservices.ReviewAPI.API.Contracts.Responses;
using microservices.ReviewAPI.API.Contracts.Requests;
using microservices.ReviewAPI.Domain.Models;

namespace microservices.ReviewAPI.Domain.Interfaces.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponse>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(Guid Id);
        Task<ReviewResponse> CreateNewReviewAsync(ReviewRequest request);
        Task<Guid> UpdateSingleReviewByIdAsync(Guid id, ReviewRequest request);
        Task<Guid> DeleteSingleReviewByIdAsync(Guid id);
    }
}
