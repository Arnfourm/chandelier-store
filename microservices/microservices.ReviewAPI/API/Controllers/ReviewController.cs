using microservices.ReviewAPI.API.Contracts.Requests;
using microservices.ReviewAPI.API.Contracts.Responses;
using microservices.ReviewAPI.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace microservices.OrderAPI.API.Controllers
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

        [HttpPost]
        public async Task<ActionResult<ReviewResponse>> CreateOrderAsync([FromForm] ReviewRequest reviewRequest)
        {
            ReviewResponse reviewResponse = await _reviewService.CreateNewReviewAsync(reviewRequest);

            return Ok(reviewResponse);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateReviewAsync(Guid id, [FromBody] ReviewRequest reviewRequest)
        {
            await _reviewService.UpdateSingleReviewByIdAsync(id, reviewRequest);

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteReviewAsync(Guid id)
        {
            await _reviewService.DeleteSingleReviewByIdAsync(id);

            return Ok();
        }
    }
}