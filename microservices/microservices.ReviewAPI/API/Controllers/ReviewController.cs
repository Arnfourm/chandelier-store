using microservices.ReviewAPI.API.Contracts.Requests;
using microservices.ReviewAPI.API.Contracts.Responses;
using microservices.ReviewAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace microservices.ReviewAPI.API.Controllers
{
    [ApiController]
    [Route("api/Review/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetAllReviewsAsync()
        {
            IEnumerable<ReviewResponse> response = await _reviewService.GetAllReviewsAsync();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ReviewResponse>> GetReviewByIdAsync(Guid id)
        {
            ReviewResponse reviewResponse = await _reviewService.GetReviewByIdAsync(id);
            return Ok(reviewResponse);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetReviewsByUserIdAsync(Guid userId)
        {
            IEnumerable<ReviewResponse> response = await _reviewService.GetReviewsByUserIdAsync(userId);
            return Ok(response);
        }

        [HttpGet("product/{productId:guid}")]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetReviewsByProductIdAsync(Guid productId)
        {
            IEnumerable<ReviewResponse> response = await _reviewService.GetReviewsByProductIdAsync(productId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ReviewResponse>> CreateReviewAsync([FromForm] ReviewRequest reviewRequest)
        {
            ReviewResponse reviewResponse = await _reviewService.CreateNewReviewAsync(reviewRequest);
            return Ok(reviewResponse);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteReviewAsync(Guid id)
        {
            await _reviewService.DeleteSingleReviewByIdAsync(id);
            return Ok();
        }
    }
}
