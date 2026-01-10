using Moq;
using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Services;

namespace TestMicroservices
{
    [TestFixture]
    public class FavouritesServiceTests
    {
        private Mock<IFavouritesDAO> _mockFavouritesDAO;
        private FavouritesService _favouritesService;
        private Guid _testUserId;
        private Guid _testProductId;

        [SetUp]
        public void Setup()
        {
            _mockFavouritesDAO = new Mock<IFavouritesDAO>();
            _favouritesService = new FavouritesService(_mockFavouritesDAO.Object);
            _testUserId = Guid.Parse("019b5c47-0653-7d58-aad9-6104a4f57435");
            _testProductId = Guid.Parse("019b5fc0-2feb-7186-a76c-175910d1abad");
        }

        [Test]
        public async Task GetFavouriteProductsAsync()
        {
            var expectedProductIds = new List<Guid> { _testProductId, Guid.NewGuid() };
            _mockFavouritesDAO
                .Setup(dao => dao.GetFavouriteProductIds(_testUserId))
                .ReturnsAsync(expectedProductIds);

            var result = await _favouritesService.GetFavouriteProductsAsync(_testUserId);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.EqualTo(expectedProductIds));
            _mockFavouritesDAO.Verify(dao => dao.GetFavouriteProductIds(_testUserId), Times.Once);
        }

        [Test]
        public async Task GetEmptyFavouriteProductsAsync()
        {
            var emptyList = new List<Guid>();
            _mockFavouritesDAO
                .Setup(dao => dao.GetFavouriteProductIds(_testUserId))
                .ReturnsAsync(emptyList);

            var result = await _favouritesService.GetFavouriteProductsAsync(_testUserId);

            Assert.That(result, Is.Empty);
            _mockFavouritesDAO.Verify(dao => dao.GetFavouriteProductIds(_testUserId), Times.Once);
        }

        [Test]
        public async Task GetFavouritesCountAsync()
        {
            int expectedCount = 5;
            _mockFavouritesDAO
                .Setup(dao => dao.GetFavouritesCount(_testUserId))
                .ReturnsAsync(expectedCount);

            var result = await _favouritesService.GetFavouritesCountAsync(_testUserId);

            Assert.That(result, Is.EqualTo(expectedCount));
            _mockFavouritesDAO.Verify(dao => dao.GetFavouritesCount(_testUserId), Times.Once);
        }

        [Test]
        public async Task GetZeroFavouritesCountAsync()
        {
            _mockFavouritesDAO
                .Setup(dao => dao.GetFavouritesCount(_testUserId))
                .ReturnsAsync(0);

            var result = await _favouritesService.GetFavouritesCountAsync(_testUserId);

            Assert.That(result, Is.EqualTo(0));
            _mockFavouritesDAO.Verify(dao => dao.GetFavouritesCount(_testUserId), Times.Once);
        }

        [Test]
        public async Task AddProductToFavouritesAsync()
        {
            _mockFavouritesDAO
                .Setup(dao => dao.IsProductInFavourites(_testUserId, _testProductId))
                .ReturnsAsync(false);

            await _favouritesService.AddProductToFavouritesAsync(_testUserId, _testProductId);

            _mockFavouritesDAO.Verify(dao => dao.IsProductInFavourites(_testUserId, _testProductId), Times.Once);
            _mockFavouritesDAO.Verify(dao => dao.AddProductToFavourites(_testUserId, _testProductId), Times.Once);
        }

        [Test]
        public void AddDuplicateProductToFavouritesAsync()
        {
            _mockFavouritesDAO
                .Setup(dao => dao.IsProductInFavourites(_testUserId, _testProductId))
                .ReturnsAsync(true);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await _favouritesService.AddProductToFavouritesAsync(_testUserId, _testProductId));

            Assert.That(ex.Message, Does.Contain($"Product with id {_testProductId} is already in favourites"));
            _mockFavouritesDAO.Verify(dao => dao.AddProductToFavourites(_testUserId, _testProductId), Times.Never);
        }

        [Test]
        public void AddInvalidProductToFavouritesAsync()
        {
            _mockFavouritesDAO
                .Setup(dao => dao.IsProductInFavourites(_testUserId, _testProductId))
                .ThrowsAsync(new Exception("Database error"));

            Assert.ThrowsAsync<Exception>(async () =>
                await _favouritesService.AddProductToFavouritesAsync(_testUserId, _testProductId));
        }

        [Test]
        public async Task RemoveProductFromFavouritesAsync()
        {
            await _favouritesService.RemoveProductFromFavouritesAsync(_testUserId, _testProductId);

            _mockFavouritesDAO.Verify(dao => dao.RemoveProductFromFavourites(_testUserId, _testProductId), Times.Once);
        }

        [Test]
        public void RemoveInvalidProductFromFavouritesAsync()
        {
            _mockFavouritesDAO
                .Setup(dao => dao.RemoveProductFromFavourites(_testUserId, _testProductId))
                .ThrowsAsync(new Exception("Database error"));

            Assert.ThrowsAsync<Exception>(async () =>
                await _favouritesService.RemoveProductFromFavouritesAsync(_testUserId, _testProductId));
        }

        [Test]
        public async Task CheckTrueIsProductInFavouritesAsync()
        {
            _mockFavouritesDAO
                .Setup(dao => dao.IsProductInFavourites(_testUserId, _testProductId))
                .ReturnsAsync(true);

            var result = await _favouritesService.IsProductInFavouritesAsync(_testUserId, _testProductId);

            Assert.That(result, Is.True);
            _mockFavouritesDAO.Verify(dao => dao.IsProductInFavourites(_testUserId, _testProductId), Times.Once);
        }

        [Test]
        public async Task CheckFalseIsProductInFavouritesAsync()
        {
            _mockFavouritesDAO
                .Setup(dao => dao.IsProductInFavourites(_testUserId, _testProductId))
                .ReturnsAsync(false);

            var result = await _favouritesService.IsProductInFavouritesAsync(_testUserId, _testProductId);

            Assert.That(result, Is.False);
            _mockFavouritesDAO.Verify(dao => dao.IsProductInFavourites(_testUserId, _testProductId), Times.Once);
        }
    }
}