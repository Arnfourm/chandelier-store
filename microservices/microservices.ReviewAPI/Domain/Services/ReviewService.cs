using System.Net.Http.Headers;
using microservices.ReviewAPI.API.Contracts.Requests;
using microservices.ReviewAPI.API.Contracts.Responses;
using microservices.ReviewAPI.Domain.Interfaces.DAO;
using microservices.ReviewAPI.Domain.Interfaces.Services;
using microservices.ReviewAPI.Domain.Models;

namespace microservices.ReviewAPI.Domain.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewDAO _reviewDAO;

        private readonly ITokenService _tokenService;

        private readonly string _userService;
        private readonly string _catalogMicroservice;
        private readonly string _orderMicroservice;

        public ReviewService(IReviewDAO reviewDAO, ITokenService tokenService, IConfiguration config)
        {
            _reviewDAO = reviewDAO;

            _tokenService = tokenService;

            _userService = config["Microservices:UserMicroservice:Url"]
                ?? throw new ArgumentException("User microservice url is null");
            _catalogMicroservice = config["Microservices:CatalogMicroservice:Url"]
                ?? throw new ArgumentException("User microservice url is null");
            _orderMicroservice = config["Microservices:OrderMicroservice:Url"]
                ?? throw new ArgumentException("User microservice url is null");
        }

        public async Task<IEnumerable<ReviewResponse>> GetAllReviewsAsync()
        {
            string token = await _tokenService.GetTokenAsync();

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    var userRequest = new HttpRequestMessage(HttpMethod.Get, $"{_userService}/User/");

                    userRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage userResponse = await httpClient.SendAsync(userRequest);

                    if (!userResponse.IsSuccessStatusCode) 
                        throw new Exception($"Can't send request to user service");
                }   
            }

            List<Review> reviews = await _reviewDAO.GetReviewsAsync();

            IEnumerable<ReviewResponse> response = reviews
                .Select(review =>
                {
                    return new ReviewResponse
                    (
                        review.GetId(),
                        review.GetRate(),
                        review.GetContent(),
                        review.GetCreationDate()
                    );
                });

            return response;
        }

        public async Task<ReviewResponse> GetReviewByIdAsync(Guid id)
        {
            Review review = await _reviewDAO.GetReviewByIdAsync(id);

            return new ReviewResponse(
                review.GetId(),
                review.GetRate(),
                review.GetContent(),
                review.GetCreationDate()
            );
        }

        public async Task<IEnumerable<ReviewResponse>> GetReviewsByProductIdAsync(Guid productId)
        {
            List<Review> reviews = await _reviewDAO.GetReviewsByProductIdAsync(productId);

            return reviews.Select(review => new ReviewResponse(
                review.GetId(),
                review.GetRate(),
                review.GetContent(),
                review.GetCreationDate()
            ));
        }

        public async Task<IEnumerable<ReviewResponse>> GetReviewsByUserIdAsync(Guid userId)
        {
            List<Review> reviews = await _reviewDAO.GetReviewsByUserIdAsync(userId);

            return reviews.Select(review => new ReviewResponse(
                review.GetId(),
                review.GetRate(),
                review.GetContent(),
                review.GetCreationDate()
            ));
        }

        public async Task<ReviewResponse> CreateNewReviewAsync(ReviewRequest request)
        {
            string token = await _tokenService.GetTokenAsync();

            Review newReview = new Review(request.UserId, request.ProductId, request.OrderId, request.Rate, request.Content, DateTime.UtcNow);
            await _reviewDAO.CreateReviewAsync(newReview);

            return new ReviewResponse(
                newReview.GetId(),
                newReview.GetRate(),
                newReview.GetContent(),
                newReview.GetCreationDate()
            );
        }

        public async Task<Guid> DeleteSingleReviewByIdAsync(Guid id)
        {
            string token = await _tokenService.GetTokenAsync();

            Review reviewToDelete = await _reviewDAO.GetReviewByIdAsync(id);
            if (reviewToDelete == null)
                throw new Exception($"Review with id: {id} doesn't exist");

            await _reviewDAO.DeleteReview(id);

            return id;
        }
    }
}
