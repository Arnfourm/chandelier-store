using Moq;
using microservices.OrderAPI.Domain.Interfaces.DAO;
using microservices.OrderAPI.Domain.Interfaces.Services;
using microservices.OrderAPI.Domain.Services;
using microservices.OrderAPI.Domain.Models;
using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;
using microservices.OrderAPI.Domain.DTO.Responses;
using Microsoft.Extensions.Configuration;

namespace TestMicroservices
{
    [TestFixture]
    public class OrderProductServiceTests
    {
        private Mock<IOrderProductDAO> _mockOrderProductDAO;
        private Mock<IOrderDAO> _mockOrderDAO;
        private Mock<ITokenService> _mockTokenService;
        private Mock<IConfiguration> _mockConfig;
        private OrderProductService _orderProductService;

        private Guid _testOrderId;
        private Guid _testProductId;
        private Order _testOrder;
        private OrderProduct _testOrderProduct;
        private OrderProductRequest _validOrderProductRequest;

        [SetUp]
        public void Setup()
        {
            _mockOrderProductDAO = new Mock<IOrderProductDAO>();
            _mockOrderDAO = new Mock<IOrderDAO>();
            _mockTokenService = new Mock<ITokenService>();
            _mockConfig = new Mock<IConfiguration>();

            _testOrderId = Guid.NewGuid();
            _testProductId = Guid.NewGuid();
            _testOrder = new Order(_testOrderId, Guid.NewGuid(), 100.0m, 1, 1, DateTime.Now);
            _testOrderProduct = new OrderProduct(_testOrderId, _testProductId, 50.0m, 2);
            _validOrderProductRequest = new OrderProductRequest(_testOrderId, _testProductId, 2);

            _mockConfig.Setup(x => x["Microservices:CatalogMicroservice:Url"])
                .Returns("http://localhost:5000");

            _orderProductService = new OrderProductService(
                _mockOrderProductDAO.Object,
                _mockOrderDAO.Object,
                _mockTokenService.Object,
                _mockConfig.Object);
        }

        [Test]
        public async Task GetProductsByOrderIdAsync_WithProducts_ReturnsProducts()
        {
            var orderProducts = new List<OrderProduct>
            {
                new OrderProduct(_testOrderId, Guid.NewGuid(), 50.0m, 2),
                new OrderProduct(_testOrderId, Guid.NewGuid(), 30.0m, 1)
            };

            _mockOrderProductDAO
                .Setup(dao => dao.GetOrderProductByOrderId(_testOrderId))
                .ReturnsAsync(orderProducts);

            var result = await _orderProductService.GetProductsByOrderIdAsync(_testOrderId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            _mockOrderProductDAO.Verify(dao => dao.GetOrderProductByOrderId(_testOrderId), Times.Once);
        }

        [Test]
        public async Task GetProductsByOrderIdAsync_NoProducts_ReturnsEmptyList()
        {
            var emptyProducts = new List<OrderProduct>();
            _mockOrderProductDAO
                .Setup(dao => dao.GetOrderProductByOrderId(_testOrderId))
                .ReturnsAsync(emptyProducts);

            var result = await _orderProductService.GetProductsByOrderIdAsync(_testOrderId);

            Assert.That(result, Is.Empty);
            _mockOrderProductDAO.Verify(dao => dao.GetOrderProductByOrderId(_testOrderId), Times.Once);
        }

        [Test]
        public async Task RemoveProductFromOrderAsync_ValidIds_RemovesProductSuccessfully()
        {
            var products = new List<OrderProduct> { _testOrderProduct };

            _mockOrderProductDAO
                .Setup(dao => dao.GetOrderProductByOrderId(_testOrderId))
                .ReturnsAsync(products);

            _mockOrderDAO
                .Setup(dao => dao.GetOrderById(_testOrderId))
                .ReturnsAsync(_testOrder);

            await _orderProductService.RemoveProductFromOrderAsync(_testOrderId, _testProductId);

            _mockOrderProductDAO.Verify(dao => dao.DeleteOrderProductByBothIds(_testOrderId, _testProductId), Times.Once);
            _mockOrderDAO.Verify(dao => dao.GetOrderById(_testOrderId), Times.Once);
            _mockOrderDAO.Verify(dao => dao.UpdateOrder(It.IsAny<Order>()), Times.Once);
        }

        [Test]
        public void RemoveProductFromOrderAsync_ProductNotFound_ThrowsException()
        {
            var products = new List<OrderProduct>();

            _mockOrderProductDAO
                .Setup(dao => dao.GetOrderProductByOrderId(_testOrderId))
                .ReturnsAsync(products);

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _orderProductService.RemoveProductFromOrderAsync(_testOrderId, _testProductId));

            Assert.That(ex.Message, Does.Contain("Product not found in order"));
        }

        [Test]
        public void RemoveProductFromOrderAsync_OrderNotFound_ThrowsException()
        {
            var products = new List<OrderProduct> { _testOrderProduct };

            _mockOrderProductDAO
                .Setup(dao => dao.GetOrderProductByOrderId(_testOrderId))
                .ReturnsAsync(products);

            _mockOrderDAO
                .Setup(dao => dao.GetOrderById(_testOrderId))
                .ThrowsAsync(new Exception("Order not found"));

            Assert.ThrowsAsync<Exception>(async () =>
                await _orderProductService.RemoveProductFromOrderAsync(_testOrderId, _testProductId));
        }
    }
}