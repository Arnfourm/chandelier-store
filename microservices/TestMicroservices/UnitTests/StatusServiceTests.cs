using Moq;
using microservices.OrderAPI.Domain.Interfaces.DAO;
using microservices.OrderAPI.Domain.Services;
using microservices.OrderAPI.Domain.Models;
using microservices.OrderAPI.API.Contracts.Requests;
using microservices.OrderAPI.API.Contracts.Responses;

namespace TestMicroservices
{
    [TestFixture]
    public class StatusServiceTests
    {
        private Mock<IStatusDAO> _mockStatusDAO;
        private StatusService _statusService;
        private Status _testStatus;
        private StatusRequest _validStatusRequest;

        [SetUp]
        public void Setup()
        {
            _mockStatusDAO = new Mock<IStatusDAO>();
            _statusService = new StatusService(_mockStatusDAO.Object);

            _testStatus = new Status(1, "Processing");
            _validStatusRequest = new StatusRequest("Delivered");
        }

        [Test]
        public async Task GetAllStatusResponses()
        {
            var statuses = new List<Status>
            {
                new Status(1, "Processing"),
                new Status(2, "Shipped"),
                new Status(3, "Delivered")
            };

            _mockStatusDAO
                .Setup(dao => dao.GetStatuses())
                .ReturnsAsync(statuses);

            var result = await _statusService.GetAllStatusResponses();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(3));

            var resultList = result.ToList();
            Assert.That(resultList[0].Id, Is.EqualTo(1));
            Assert.That(resultList[0].Title, Is.EqualTo("Processing"));

            _mockStatusDAO.Verify(dao => dao.GetStatuses(), Times.Once);
        }

        [Test]
        public async Task GetEmptyAllStatusResponse()
        {
            var emptyStatuses = new List<Status>();
            _mockStatusDAO
                .Setup(dao => dao.GetStatuses())
                .ReturnsAsync(emptyStatuses);

            var result = await _statusService.GetAllStatusResponses();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
            _mockStatusDAO.Verify(dao => dao.GetStatuses(), Times.Once);
        }

        [Test]
        public async Task GetStatusById()
        {
            int statusId = 1;
            _mockStatusDAO
                .Setup(dao => dao.GetStatusById(statusId))
                .ReturnsAsync(_testStatus);

            var result = await _statusService.GetStatusById(statusId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.GetId(), Is.EqualTo(statusId));
            Assert.That(result.GetTitle(), Is.EqualTo("Processing"));
            _mockStatusDAO.Verify(dao => dao.GetStatusById(statusId), Times.Once);
        }

        [Test]
        public void GetStatusByInvalidId()
        {
            int invalidId = 999;
            _mockStatusDAO
                .Setup(dao => dao.GetStatusById(invalidId))
                .ThrowsAsync(new KeyNotFoundException($"Status with id {invalidId} not found"));

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _statusService.GetStatusById(invalidId));

            _mockStatusDAO.Verify(dao => dao.GetStatusById(invalidId), Times.Once);
        }

        [Test]
        public async Task CreateNewStatus()
        {
            var createdStatus = new Status(4, "Delivered");

            _mockStatusDAO
                .Setup(dao => dao.CreateStatus(It.IsAny<Status>()))
                .ReturnsAsync(createdStatus);

            var result = await _statusService.CreateNewStatus(_validStatusRequest);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(4));
            Assert.That(result.Title, Is.EqualTo("Delivered"));

            _mockStatusDAO.Verify(dao => dao.CreateStatus(It.Is<Status>(s =>
                s.GetTitle() == _validStatusRequest.Title)), Times.Once);
        }

        [Test]
        public void CreateNewInvalidStatus()
        {
            var invalidRequest = new StatusRequest("");

            _mockStatusDAO
                .Setup(dao => dao.CreateStatus(It.IsAny<Status>()))
                .ThrowsAsync(new ArgumentException("Title cannot be empty"));

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await _statusService.CreateNewStatus(invalidRequest));
        }

        [Test]
        public async Task UpdateStatusById()
        {
            int statusId = 1;
            _mockStatusDAO
                .Setup(dao => dao.UpdateStatus(It.IsAny<Status>()))
                .Returns(Task.CompletedTask);

            await _statusService.UpdateStatusById(statusId, _validStatusRequest);

            _mockStatusDAO.Verify(dao => dao.UpdateStatus(It.Is<Status>(s =>
                s.GetId() == statusId &&
                s.GetTitle() == _validStatusRequest.Title)), Times.Once);
        }

        [Test]
        public void UpdateStatusByInvalidId()
        {
            int invalidId = 999;
            _mockStatusDAO
                .Setup(dao => dao.UpdateStatus(It.IsAny<Status>()))
                .ThrowsAsync(new KeyNotFoundException($"Status with id {invalidId} not found"));

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _statusService.UpdateStatusById(invalidId, _validStatusRequest));

            _mockStatusDAO.Verify(dao => dao.UpdateStatus(It.IsAny<Status>()), Times.Once);
        }

        [Test]
        public void InvalidUpdateStatusById()
        {
            int statusId = 1;
            var invalidRequest = new StatusRequest("");

            _mockStatusDAO
                .Setup(dao => dao.UpdateStatus(It.IsAny<Status>()))
                .ThrowsAsync(new ArgumentException("Title cannot be empty"));

            Assert.ThrowsAsync<ArgumentException>(async () =>
                await _statusService.UpdateStatusById(statusId, invalidRequest));
        }

        [Test]
        public async Task DeleteStatusById()
        {
            int statusId = 1;
            _mockStatusDAO
                .Setup(dao => dao.DeleteStatus(statusId))
                .Returns(Task.CompletedTask);

            await _statusService.DeleteStatusById(statusId);

            _mockStatusDAO.Verify(dao => dao.DeleteStatus(statusId), Times.Once);
        }

        [Test]
        public void DeleteStatusByInvalidId()
        {
            int invalidId = 999;
            _mockStatusDAO
                .Setup(dao => dao.DeleteStatus(invalidId))
                .ThrowsAsync(new KeyNotFoundException($"Status with id {invalidId} not found"));

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _statusService.DeleteStatusById(invalidId));

            _mockStatusDAO.Verify(dao => dao.DeleteStatus(invalidId), Times.Once);
        }
    }
}