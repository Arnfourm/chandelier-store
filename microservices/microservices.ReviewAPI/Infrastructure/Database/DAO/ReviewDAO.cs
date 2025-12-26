using microservices.ReviewAPI.Domain.Interfaces.DAO;
using microservices.ReviewAPI.Domain.Models;
using microservices.ReviewAPI.Infrastructure.Database.Contexts;
using microservices.ReviewAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.ReviewAPI.Infrastructure.Database.DAO
{
    public class ReviewDAO : IReviewDAO
    {
        private readonly ReviewDbContext _reviewDbContext;

        public ReviewDAO(ReviewDbContext reviewDbContext)
        {
            _reviewDbContext = reviewDbContext;
        }

        public async Task<List<Review>> GetReviewsAsync()
        {
            return await _reviewDbContext.Reviews
                .Select(reviewEntity => new Review
                (
                    reviewEntity.Id,
                    reviewEntity.UserId,
                    reviewEntity.ProductId,
                    reviewEntity.OrderId,
                    reviewEntity.Rate,
                    reviewEntity.Content,
                    reviewEntity.CreationDate
                )).ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(Guid id)
        {
            var reviewEntity = await _reviewDbContext.Reviews.FindAsync(id);

            if (reviewEntity == null)
            {
                throw new ArgumentException($"Review with id: {id} not found");
            }

            return new Review
            (
                reviewEntity.Id,
                reviewEntity.UserId,
                reviewEntity.ProductId,
                reviewEntity.OrderId,
                reviewEntity.Rate,
                reviewEntity.Content,
                reviewEntity.CreationDate
            );
        }

        public async Task<List<Review>> GetReviewByIdsAsync(List<Guid> ids)
        {
            return await _reviewDbContext.Reviews
                .Where(reviewEntity => ids.Contains(reviewEntity.Id))
                .Select(reviewEntity => new Review
                (
                    reviewEntity.Id,
                    reviewEntity.UserId,
                    reviewEntity.ProductId,
                    reviewEntity.OrderId,
                    reviewEntity.Rate,
                    reviewEntity.Content,
                    reviewEntity.CreationDate
                )).ToListAsync();
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            ReviewEntity reviewEntity = new ReviewEntity
            {
                UserId = review.GetUserId(),
                ProductId = review.GetProductId(),
                OrderId = review.GetOrderId(),
                Rate = review.GetRate(),
                Content = review.GetContent(),
                CreationDate = review.GetCreationDate()
            };

            await _reviewDbContext.Reviews.AddAsync(reviewEntity);
            try
            {
                await _reviewDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error while trying to add new review. Error message:\n{ex.Message}", ex);
            }

            return new Review
            (
                reviewEntity.Id,
                reviewEntity.UserId,
                reviewEntity.ProductId,
                reviewEntity.OrderId,
                reviewEntity.Rate,
                reviewEntity.Content,
                reviewEntity.CreationDate
            );
        }

        public async Task<Guid> UpdateReviewAsync(Review review)
        {
            await _reviewDbContext.Reviews
                .Where(reviewEntity => reviewEntity.Id == review.GetId())
                .ExecuteUpdateAsync(reviewSetters => reviewSetters
                    .SetProperty(reviewEntity => reviewEntity.UserId, review.GetUserId())
                    .SetProperty(reviewEntity => reviewEntity.ProductId, review.GetProductId())
                    .SetProperty(reviewEntity => reviewEntity.OrderId, review.GetOrderId())
                    .SetProperty(reviewEntity => reviewEntity.Rate, review.GetRate())
                    .SetProperty(reviewEntity => reviewEntity.Content, review.GetContent()));

            try
            {
                await _reviewDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to update review. Error message:\n{ex.Message}", ex);
            }

            return review.GetId();
        }

        public async Task DeleteReview(Guid id)
        {
            await _reviewDbContext.Reviews.Where(reviewEntity => reviewEntity.Id == id).ExecuteDeleteAsync();
            try
            {
                await _reviewDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error while trying to delete review. Error message:\n{ex.Message}", ex);
            }
        }
    }
}
