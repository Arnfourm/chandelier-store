using Moq;
using microservices.ReviewAPI.Domain.Interfaces.DAO;
using microservices.ReviewAPI.Domain.Interfaces.Services;
using microservices.ReviewAPI.Domain.Services;
using microservices.ReviewAPI.API.Contracts.Requests;
using microservices.ReviewAPI.API.Contracts.Responses;
using microservices.ReviewAPI.Domain.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestMicroservices
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private Mock<IReviewDAO> _mockReviewDAO;
        private ReviewService _reviewService;

        private Guid _testReviewId;
        private Guid _testUserId;
        private Guid _testProductId;
        private Guid _testOrderId;
        private int _testRate;
        private string _testContent;
        private DateTime _testCreationDate;

        [SetUp]
        public void Setup()
        {
            _mockReviewDAO = new Mock<IReviewDAO>();

            var inMemorySettings = new Dictionary<string, string> {
                {"Microservices:UserMicroservice:Url", "http://test-user.com"},
                {"Microservices:CatalogMicroservice:Url", "http://test-catalog.com"},
                {"Microservices:OrderMicroservice:Url", "http://test-order.com"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var mockTokenService = new Mock<ITokenService>();

            _reviewService = new ReviewService(
                _mockReviewDAO.Object,
                mockTokenService.Object,
                configuration
            );

            _testReviewId = Guid.NewGuid();
            _testUserId = Guid.NewGuid();
            _testProductId = Guid.NewGuid();
            _testOrderId = Guid.NewGuid();
            _testRate = 5;
            _testContent = "Excellent product!";
            _testCreationDate = DateTime.UtcNow;
        }

        private Review CreateTestReview(Guid? userId = null, Guid? productId = null)
        {
            return new Review(
                userId ?? _testUserId,
                productId ?? _testProductId,
                _testOrderId,
                _testRate,
                _testContent,
                _testCreationDate
            );
        }

        [Test]
        public async Task GetReviewByIdAsync()
        {
            var testReview = CreateTestReview();

            _mockReviewDAO
                .Setup(dao => dao.GetReviewByIdAsync(_testReviewId))
                .ReturnsAsync(testReview);

            var result = await _reviewService.GetReviewByIdAsync(_testReviewId);

            Assert.That(result.Id, Is.EqualTo(testReview.GetId()));
            Assert.That(result.Rate, Is.EqualTo(testReview.GetRate()));
            Assert.That(result.Content, Is.EqualTo(testReview.GetContent()));
            _mockReviewDAO.Verify(dao => dao.GetReviewByIdAsync(_testReviewId), Times.Once);
        }

        [Test]
        public void GetInvalidReviewByIdAsync()
        {
            var wrongId = Guid.NewGuid();

            _mockReviewDAO
                .Setup(dao => dao.GetReviewByIdAsync(wrongId))
                .ThrowsAsync(new KeyNotFoundException($"Review with id {wrongId} not found"));

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _reviewService.GetReviewByIdAsync(wrongId));

            Assert.That(ex.Message, Does.Contain(wrongId.ToString()));
        }

        [Test]
        public void GetNullReviewByIdAsync()
        {
            var wrongId = Guid.NewGuid();

            _mockReviewDAO
                .Setup(dao => dao.GetReviewByIdAsync(wrongId))
                .ReturnsAsync((Review)null);

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _reviewService.GetReviewByIdAsync(wrongId));

            Assert.That(ex.Message, Does.Contain(wrongId.ToString()));
            Assert.That(ex.Message, Does.Contain("not found"));

            _mockReviewDAO.Verify(dao => dao.GetReviewByIdAsync(wrongId), Times.Once);
        }

        [Test]
        public async Task GetReviewsByProductIdAsync()
        {
            var productId = Guid.NewGuid();
            var testReviews = new List<Review>
            {
                CreateTestReview(productId: productId),
                CreateTestReview(productId: productId)
            };

            _mockReviewDAO
                .Setup(dao => dao.GetReviewsByProductIdAsync(productId))
                .ReturnsAsync(testReviews);

            var result = await _reviewService.GetReviewsByProductIdAsync(productId);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(2));
            _mockReviewDAO.Verify(dao => dao.GetReviewsByProductIdAsync(productId), Times.Once);
        }

        [Test]
        public void GetInvalidReviewsByProductIdAsync()
        {
            var productId = Guid.NewGuid();

            _mockReviewDAO
                .Setup(dao => dao.GetReviewsByProductIdAsync(productId))
                .ThrowsAsync(new Exception($"Product with id {productId} not found"));

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _reviewService.GetReviewsByProductIdAsync(productId));

            Assert.That(ex.Message, Does.Contain(productId.ToString()));
        }

        [Test]
        public async Task GetEmptyReviewsByProductIdAsync()
        {
            var productId = Guid.NewGuid();

            _mockReviewDAO
                .Setup(dao => dao.GetReviewsByProductIdAsync(productId))
                .ReturnsAsync(new List<Review>());

            var result = await _reviewService.GetReviewsByProductIdAsync(productId);

            Assert.That(result, Is.Empty);
            _mockReviewDAO.Verify(dao => dao.GetReviewsByProductIdAsync(productId), Times.Once);
        }

        [Test]
        public async Task GetReviewsByUserIdAsync()
        {
            var userId = Guid.NewGuid();
            var testReviews = new List<Review>
            {
                CreateTestReview(userId: userId),
                CreateTestReview(userId: userId)
            };

            _mockReviewDAO
                .Setup(dao => dao.GetReviewsByUserIdAsync(userId))
                .ReturnsAsync(testReviews);

            var result = await _reviewService.GetReviewsByUserIdAsync(userId);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(2));
            _mockReviewDAO.Verify(dao => dao.GetReviewsByUserIdAsync(userId), Times.Once);
        }

        [Test]
        public void GetInvalidReviewsByUserIdAsync()
        {
            var userId = Guid.NewGuid();

            _mockReviewDAO
                .Setup(dao => dao.GetReviewsByUserIdAsync(userId))
                .ThrowsAsync(new Exception($"User with id {userId} not found"));

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _reviewService.GetReviewsByUserIdAsync(userId));

            Assert.That(ex.Message, Does.Contain(userId.ToString()));
        }

        [Test]
        public async Task GetEmptyReviewsByUserIdAsync()
        {
            var userId = Guid.NewGuid();

            _mockReviewDAO
                .Setup(dao => dao.GetReviewsByUserIdAsync(userId))
                .ReturnsAsync(new List<Review>());

            var result = await _reviewService.GetReviewsByUserIdAsync(userId);

            Assert.That(result, Is.Empty);
            _mockReviewDAO.Verify(dao => dao.GetReviewsByUserIdAsync(userId), Times.Once);
        }
    }
}