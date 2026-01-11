using Moq;
using microservices.OrderAPI.Domain.Interfaces.DAO;
using microservices.OrderAPI.Domain.Interfaces.Services;
using microservices.OrderAPI.Domain.Services;
using microservices.OrderAPI.Domain.Models;
using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using Microsoft.Extensions.Configuration;

namespace TestMicroservices
{
    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<IOrderDAO> _mockOrderDAO;
        private Mock<IDeliveryTypeService> _mockDeliveryTypeService;
        private Mock<IStatusService> _mockStatusService;
        private Mock<ITokenService> _mockTokenService;
        private Mock<IConfiguration> _mockConfig;
        private OrderService _orderService;

        private Guid _testOrderId;
        private Guid _testUserId;
        private Order _testOrder;
        private StatusResponse _testStatusResponse;
        private DeliveryTypeResponse _testDeliveryTypeResponse;
        private OrderRequest _validOrderRequest;

        [SetUp]
        public void Setup()
        {
            _mockOrderDAO = new Mock<IOrderDAO>();
            _mockDeliveryTypeService = new Mock<IDeliveryTypeService>();
            _mockStatusService = new Mock<IStatusService>();
            _mockTokenService = new Mock<ITokenService>();
            _mockConfig = new Mock<IConfiguration>();

            _testOrderId = Guid.NewGuid();
            _testUserId = Guid.NewGuid();

            _testOrder = new Order(_testOrderId, _testUserId, 100.0m, 1, 1, DateTime.UtcNow);
            _testStatusResponse = new StatusResponse(1, "Processing");
            _testDeliveryTypeResponse = new DeliveryTypeResponse(1, "Standard");
            _validOrderRequest = new OrderRequest(_testUserId, 100.0m, 1, 1);

            _mockConfig.Setup(x => x["Microservices:UserMicroservice:Url"])
                .Returns("http://localhost:5001");

            _orderService = new OrderService(
                _mockOrderDAO.Object,
                _mockDeliveryTypeService.Object,
                _mockStatusService.Object,
                _mockTokenService.Object,
                _mockConfig.Object
            );
        }

        [Test]
        public async Task GetOrderByIdAsync()
        {
            _mockOrderDAO
                .Setup(dao => dao.GetOrderById(_testOrderId))
                .ReturnsAsync(_testOrder);

            _mockStatusService
                .Setup(s => s.GetStatusResponseByIdAsync(1))
                .ReturnsAsync(_testStatusResponse);

            _mockDeliveryTypeService
                .Setup(d => d.GetDeliveryTypeResponseByIdAsync(1))
                .ReturnsAsync(_testDeliveryTypeResponse);

            var result = await _orderService.GetOrderByIdAsync(_testOrderId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(_testOrderId));
            Assert.That(result.TotalAmount, Is.EqualTo(100.0m));
            _mockOrderDAO.Verify(dao => dao.GetOrderById(_testOrderId), Times.Once);
        }

        [Test]
        public void GetOrderByInvalidIdAsync()
        {
            Guid invalidId = Guid.NewGuid();
            _mockOrderDAO
                .Setup(dao => dao.GetOrderById(invalidId))
                .ThrowsAsync(new KeyNotFoundException($"Order with id {invalidId} not found"));

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _orderService.GetOrderByIdAsync(invalidId));
        }

        [Test]
        public async Task DeleteSingleOrderByIdAsync()
        {
            await _orderService.DeleteSingleOrderByIdAsync(_testOrderId);

            _mockOrderDAO.Verify(dao => dao.DeleteOrder(_testOrderId), Times.Once);
        }

        [Test]
        public void DeleteSingleOrderByInvalidIdAsync()
        {
            Guid invalidId = Guid.NewGuid();
            _mockOrderDAO
                .Setup(dao => dao.DeleteOrder(invalidId))
                .ThrowsAsync(new KeyNotFoundException($"Order with id {invalidId} not found"));

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _orderService.DeleteSingleOrderByIdAsync(invalidId));
        }
    }
}