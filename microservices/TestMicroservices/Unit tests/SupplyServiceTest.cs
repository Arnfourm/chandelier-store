
using microservice.SupplyAPI.API.Contracts.Requests;
using microservice.SupplyAPI.Domain.Interfaces.DAO;
using microservice.SupplyAPI.Domain.Interfaces.Services;
using microservice.SupplyAPI.Domain.Models;
using microservice.SupplyAPI.Domain.Services;
using Moq;

namespace TestMicroservices
{
    [TestFixture]
    public class SupplyServiceTest
    {
        private Mock<ISupplyDAO> _mockSupplyDAO;
        private Mock<ISupplierService> _mockSupplierService;
        private SupplyService _supplyService;

        private List<Supply> _supplies;
        private List<Supplier> _suppliers;
        private List<DeliveryType> _deliveryTypes;

        private SupplyRequest _validSupplyRequest;

        [SetUp]
        public void Setup()
        {
            _mockSupplyDAO = new Mock<ISupplyDAO>();
            _mockSupplierService = new Mock<ISupplierService>();

            _supplyService = new SupplyService(
                _mockSupplyDAO.Object, 
                _mockSupplierService.Object
            );

            _deliveryTypes = new List<DeliveryType>
            {
                new DeliveryType(1, "ПЭК", "До склада"),
                new DeliveryType(2, "СДЭК", "До пункта выдачи"),
                new DeliveryType(3, "Курьер", "До торговой точки")
            };
            _suppliers = new List<Supplier>
            {
                new Supplier(Guid.NewGuid(), "Ситилюкс", 1),
                new Supplier(Guid.NewGuid(), "Евросвет", 2),
                new Supplier(Guid.NewGuid(), "Лофт ит", 3)
            };
            _supplies = new List<Supply>
            {
                new Supply(Guid.NewGuid(), _suppliers[0].GetId(), new DateOnly(2025, 12, 1), 1000),
                new Supply(Guid.NewGuid(), _suppliers[1].GetId(), new DateOnly(2025, 12, 5), 5000),
                new Supply(Guid.NewGuid(), _suppliers[2].GetId(), new DateOnly(2025, 12, 10), 10000)
            };

            _validSupplyRequest = new SupplyRequest
            (
                _suppliers[0].GetId(),
                15000
            );
        }

        [Test]
        public async Task GetAllSupplyResponses()
        {
            _mockSupplyDAO
                .Setup(dao => dao.GetSupplies())
                .ReturnsAsync(_supplies);

            var result = await _supplyService.GetAllSupplies();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(3));

            var resultList = result.ToList();
            Console.WriteLine(resultList[0].Supplier.Id);
            Assert.That(resultList[0].Id, Is.EqualTo(_supplies[0].GetId()));
            Assert.That(resultList[0].Supplier.Id, Is.EqualTo(_suppliers[0].GetId()));
            Assert.That(resultList[0].SupplyDate, Is.EqualTo(_supplies[0].GetSupplyDate()));
            Assert.That(resultList[0].TotalAmount, Is.EqualTo(1000));

            _mockSupplyDAO.Verify(dao => dao.GetSupplies(), Times.Once());
        }

        [Test]
        public async Task CreateNewSupply()
        {
            var newSupply = new Supply
            (
                Guid.NewGuid(),
                _suppliers[0].GetId(),
                new DateOnly(2025, 12, 1),
                1252
            );

            _mockSupplyDAO
                .Setup(dao => dao.CreateSupply(It.IsAny<Supply>()))
                .ReturnsAsync(newSupply);

            var result = await _supplyService.CreateNewSupply(_validSupplyRequest);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Supplier.Id, Is.EqualTo(_suppliers[0].GetId()));
            Assert.That(result.Supplier.Name, Is.EqualTo(_suppliers[0].GetName()));
            Assert.That(result.SupplyDate, Is.EqualTo(new DateOnly(2025, 12, 1)));
            Assert.That(result.TotalAmount, Is.EqualTo(1252));

            _mockSupplyDAO.Verify(dao => dao.CreateSupply(It.Is<Supply>(s =>
                s.GetTotalAmount() == _validSupplyRequest.TotalAmount)), Times.Once);
        }
    }
}
