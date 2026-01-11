using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using microservice.SupplyAPI.Domain.Models;
using microservice.SupplyAPI.Domain.Services;
using Moq;

namespace TestMicroservices
{
    [TestFixture]
    public class DeliveryTypeServiceTest
    {
        private Mock<IDeliveryTypeDAO> _mockDeliveryTypeDAO;
        private IDeliveryTypeService _deliveryTypeService;

        private List<DeliveryType> _deliveryTypes;

        private DeliveryTypeRequest _validDeliveryTypeRequest;

        [SetUp]
        public void Setup()
        {
            _mockDeliveryTypeDAO = new Mock<IDeliveryTypeDAO>();
            _deliveryTypeService = new DeliveryTypeService(_mockDeliveryTypeDAO.Object);

            _deliveryTypes = new List<DeliveryType>
            {
                new DeliveryType(1, "ПЭК", "До склада"),
                new DeliveryType(2, "СДЭК", "До пункта выдачи"),
                new DeliveryType(3, "Курьер", "До торговой точки")
            };

            _validDeliveryTypeRequest = new DeliveryTypeRequest("Деловые линии", "Машина до торговой точки");
        }

        [Test]
        public async Task GetAllDeliveryTypeResponses()
        {
            _mockDeliveryTypeDAO
                .Setup(dao => dao.GetDeliveryTypes())
                .ReturnsAsync(_deliveryTypes);

            var result = await _deliveryTypeService.GetAllDeliveryType();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(3));

            var resultList = result.ToList();
            Assert.That(resultList[0].Id, Is.EqualTo(1));
            Assert.That(resultList[0].Title, Is.EqualTo("ПЭК"));
            Assert.That(resultList[0].Comment, Is.EqualTo("До склада"));

            _mockDeliveryTypeDAO.Verify(dao => dao.GetDeliveryTypes(), Times.Once());
        }

        [Test]
        public async Task GetEmptyAllDeliveryTypeResponses()
        {
            var emptyDeliveryTypes = new List<DeliveryType>();
            _mockDeliveryTypeDAO
                .Setup(dao => dao.GetDeliveryTypes())
                .ReturnsAsync(emptyDeliveryTypes);

            var result = await _deliveryTypeService.GetAllDeliveryType();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);

            _mockDeliveryTypeDAO.Verify(dao => dao.GetDeliveryTypes(), Times.Once);
        }

        [Test]
        public async Task GetDeliveryTypeById()
        {
            int deliveryTypeId = 1;

            _mockDeliveryTypeDAO
                .Setup(dao => dao.GetDeliveryTypeById(deliveryTypeId))
                .ReturnsAsync(_deliveryTypes[0]);

            var result = await _deliveryTypeService.GetSingleDeliveryTypeById(deliveryTypeId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.GetId(), Is.EqualTo(deliveryTypeId));
            Assert.That(result.GetTitle(), Is.EqualTo("ПЭК"));
            Assert.That(result.GetComment(), Is.EqualTo("До склада"));

            _mockDeliveryTypeDAO.Verify(dao => dao.GetDeliveryTypeById(deliveryTypeId), Times.Once);
        }

        [Test]
        public async Task GetDeliveryTypeByInvalidId()
        {
            int invalidId = 999;
            _mockDeliveryTypeDAO
                .Setup(dao => dao.GetDeliveryTypeById(invalidId))
                .ThrowsAsync(new KeyNotFoundException($"DeliveryType with id {invalidId} not found"));

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _deliveryTypeService.GetSingleDeliveryTypeById(invalidId));

            _mockDeliveryTypeDAO.Verify(dao => dao.GetDeliveryTypeById(invalidId), Times.Once);
        }

        [Test]
        public async Task CreateNewDeliveryType()
        {
            var newDeliveryType = new DeliveryType(4, "Деловые линии", "Машина до торговой точки");

            _mockDeliveryTypeDAO
                 .Setup(dao => dao.CreateDeliveryType(It.IsAny<DeliveryType>()))
                 .ReturnsAsync(newDeliveryType);

            var result = await _deliveryTypeService.CreateNewDeliveryType(_validDeliveryTypeRequest);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(4));
            Assert.That(result.Title, Is.EqualTo("Деловые линии"));

            _mockDeliveryTypeDAO.Verify(dao => dao.CreateDeliveryType(It.Is<DeliveryType>(dt =>
                dt.GetTitle() == _validDeliveryTypeRequest.Title)), Times.Once);
        }

        [Test]
        public async Task CreateNewInvalidDeliveryType()
        {
            DeliveryTypeRequest invalidRequest = new DeliveryTypeRequest("", "");

            _mockDeliveryTypeDAO
                .Setup(dao => dao.CreateDeliveryType(It.IsAny<DeliveryType>()))
                .ThrowsAsync(new ArgumentException("Title can't be empty"));

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await _deliveryTypeService.CreateNewDeliveryType(invalidRequest));
        }

        [Test]
        public async Task UpdateDeliveryTypeById()
        {
            _mockDeliveryTypeDAO
                .Setup(dao => dao.UpdateDeliveryType(It.IsAny<DeliveryType>()))
                .Returns(Task.CompletedTask);

            await _deliveryTypeService.UpdateSingleDeliveryTypeById(_deliveryTypes[0].GetId(), _validDeliveryTypeRequest);

            _mockDeliveryTypeDAO.Verify(dao => dao.UpdateDeliveryType(It.Is<DeliveryType>(dt =>
                dt.GetId() == _deliveryTypes[0].GetId() &&
                dt.GetTitle() == _validDeliveryTypeRequest.Title &&
                dt.GetComment() == _validDeliveryTypeRequest.Comment)), Times.Once);
        }

        [Test]
        public async Task UpdateDeliveryTypeByInvalidId()
        {
            int invalidId = 999;
            _mockDeliveryTypeDAO
                .Setup(dao => dao.UpdateDeliveryType(It.IsAny<DeliveryType>()))
                .ThrowsAsync(new KeyNotFoundException($"Delivery type with id {invalidId} not found"));

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _deliveryTypeService.UpdateSingleDeliveryTypeById(invalidId, _validDeliveryTypeRequest));

            _mockDeliveryTypeDAO.Verify(dao => dao.UpdateDeliveryType(It.IsAny<DeliveryType>()));
        }

        [Test]
        public async Task InvalidUpdateDeliveryTypeById()
        {
            var invalidRequest = new DeliveryTypeRequest("", "");

            _mockDeliveryTypeDAO
                .Setup(dao => dao.UpdateDeliveryType(It.IsAny<DeliveryType>()))
                .ThrowsAsync(new KeyNotFoundException("Title can't be empty"));

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await _deliveryTypeService.UpdateSingleDeliveryTypeById(_deliveryTypes[0].GetId(), invalidRequest));
        }

        [Test]
        public async Task DeleteDeliveryTypeById()
        {
            _mockDeliveryTypeDAO
                .Setup(dao => dao.DeleteDeliveryTypeById(_deliveryTypes[0].GetId()))
                .Returns(Task.CompletedTask);

            await _deliveryTypeService.DeleteSingleDeliveryTypeById(_deliveryTypes[0].GetId());

            _mockDeliveryTypeDAO.Verify(dao => dao.DeleteDeliveryTypeById(_deliveryTypes[0].GetId()), Times.Once);
        }

        [Test]
        public async Task DeleteDeliveryTypeByInvalidId()
        {
            int invalidId = 999;
            _mockDeliveryTypeDAO
                .Setup(dao => dao.DeleteDeliveryTypeById(invalidId))
                .ThrowsAsync(new KeyNotFoundException($"Delivery Type with id {invalidId} not found"));

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _deliveryTypeService.DeleteSingleDeliveryTypeById(invalidId));

            _mockDeliveryTypeDAO.Verify(dao => dao.DeleteDeliveryTypeById(invalidId), Times.Once);
        }
    }
}
