using Moq;
using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Interfaces.Services;
using microservices.UserAPI.Domain.Services;
using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.API.Contracts.Responses;
using microservices.UserAPI.Domain.Models;
using microservices.UserAPI.Domain.Enums;

namespace TestMicroservices
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserDAO> _mockUserDAO;
        private Mock<IPasswordDAO> _mockPasswordDAO;
        private Mock<IPasswordService> _mockPasswordService;
        private UserService _userService;

        private Guid _testUserId;
        private Guid _testPasswordId;
        private string _testEmail;
        private string _testName;
        private string _testSurname;
        private string _testPassword;
        private DateOnly _testBirthday;
        private DateTime _testRegistration;

        [SetUp]
        public void Setup()
        {
            _mockUserDAO = new Mock<IUserDAO>();
            _mockPasswordDAO = new Mock<IPasswordDAO>();
            _mockPasswordService = new Mock<IPasswordService>();

            _userService = new UserService(
                _mockUserDAO.Object,
                _mockPasswordDAO.Object,
                _mockPasswordService.Object
            );

            _testUserId = Guid.NewGuid();
            _testPasswordId = Guid.NewGuid();
            _testEmail = "test@example.com";
            _testName = "John";
            _testSurname = "Doe";
            _testPassword = "password123";
            _testBirthday = new DateOnly(1990, 1, 1);
            _testRegistration = DateTime.UtcNow;
        }

        private User CreateTestUser(Guid? id = null, string? email = null)
        {
            return new User(
                id ?? _testUserId,
                email ?? _testEmail,
                _testName,
                _testSurname,
                _testBirthday,
                _testRegistration,
                _testPasswordId,
                null,
                UserRoleEnum.Client
            );
        }

        private UserResponse CreateTestUserResponse(Guid? id = null, string? email = null)
        {
            return new UserResponse(
                Id: id ?? _testUserId,
                Email: email ?? _testEmail,
                Name: _testName,
                Surname: _testSurname,
                Birthday: _testBirthday,
                Registration: _testRegistration,
                UserRole: UserRoleEnum.Client
            );
        }

        [Test]
        public async Task GetAllUsersAsync()
        {
            var testUsers = new List<User>
            {
                CreateTestUser(),
                new User(
                    Guid.NewGuid(),
                    "test2@example.com",
                    "Jane",
                    "Smith",
                    new DateOnly(1992, 5, 15),
                    DateTime.UtcNow,
                    Guid.NewGuid(),
                    null,
                    UserRoleEnum.Client
                )
            };

            _mockUserDAO
                .Setup(dao => dao.GetUsers())
                .ReturnsAsync(testUsers);

            var result = await _userService.GetAllUsersAsync();

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(2));
            _mockUserDAO.Verify(dao => dao.GetUsers(), Times.Once);
        }

        [Test]
        public async Task GetEmptyAllUsersAsync()
        {
            _mockUserDAO
                .Setup(dao => dao.GetUsers())
                .ReturnsAsync(new List<User>());

            var result = await _userService.GetAllUsersAsync();

            Assert.That(result, Is.Empty);
            _mockUserDAO.Verify(dao => dao.GetUsers(), Times.Once);
        }

        [Test]
        public async Task GetSingleUserByIdAsync()
        {
            var testUser = CreateTestUser();
            var expectedResponse = CreateTestUserResponse();

            _mockUserDAO
                .Setup(dao => dao.GetUserById(_testUserId))
                .ReturnsAsync(testUser);

            var result = await _userService.GetSingleUserByIdAsync(_testUserId);

            Assert.That(result.Id, Is.EqualTo(expectedResponse.Id));
            Assert.That(result.Email, Is.EqualTo(expectedResponse.Email));
            Assert.That(result.Name, Is.EqualTo(expectedResponse.Name));
            _mockUserDAO.Verify(dao => dao.GetUserById(_testUserId), Times.Once);
        }

        [Test]
        public void GetInvalidSingleUserByIdAsync()
        {
            var wrongId = Guid.NewGuid();

            _mockUserDAO
                .Setup(dao => dao.GetUserById(wrongId))
                .ThrowsAsync(new KeyNotFoundException($"User with id {wrongId} not found"));

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _userService.GetSingleUserByIdAsync(wrongId));

            Assert.That(ex.Message, Does.Contain(wrongId.ToString()));
        }

        [Test]
        public async Task GetSingleUserByEmailAsync()
        {
            var testUser = CreateTestUser();
            var expectedResponse = CreateTestUserResponse();

            _mockUserDAO
                .Setup(dao => dao.GetUserByEmail(_testEmail))
                .ReturnsAsync(testUser);

            var result = await _userService.GetSingleUserByEmailAsync(_testEmail);

            Assert.That(result.Id, Is.EqualTo(expectedResponse.Id));
            Assert.That(result.Email, Is.EqualTo(expectedResponse.Email));
            Assert.That(result.Name, Is.EqualTo(expectedResponse.Name));
            _mockUserDAO.Verify(dao => dao.GetUserByEmail(_testEmail), Times.Once);
        }

        [Test]
        public void GetInvalidSingleUserByEmailAsync()
        {
            var wrongEmail = "wrong@example.com";

            _mockUserDAO
                .Setup(dao => dao.GetUserByEmail(wrongEmail))
                .ThrowsAsync(new KeyNotFoundException($"User with email {wrongEmail} not found"));

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _userService.GetSingleUserByEmailAsync(wrongEmail));

            Assert.That(ex.Message, Does.Contain(wrongEmail));
        }

        [Test]
        public async Task CreateUserAsync()
        {
            var request = new UserRequest(
                Email: _testEmail,
                Name: _testName,
                Surname: _testSurname,
                Password: _testPassword,
                Birthday: _testBirthday,
                UserRole: UserRoleEnum.Client
            );

            var password = new Password(
                _testPasswordId,
                new byte[64],
                new byte[128]
            );

            _mockPasswordService
                .Setup(s => s.CreatePassword(_testPassword))
                .Returns(password);

            _mockPasswordDAO
                .Setup(dao => dao.CreatePassword(password))
                .ReturnsAsync(_testPasswordId);

            _mockUserDAO
                .Setup(dao => dao.CreateUser(It.IsAny<User>()))
                .ReturnsAsync(_testUserId);

            var result = await _userService.CreateUserAsync(request);

            Assert.That(result, Is.EqualTo(_testUserId));
            _mockPasswordService.Verify(s => s.CreatePassword(_testPassword), Times.Once);
            _mockPasswordDAO.Verify(dao => dao.CreatePassword(password), Times.Once);
            _mockUserDAO.Verify(dao => dao.CreateUser(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void CreateDuplicateUserAsync()
        {
            var request = new UserRequest(
                Email: _testEmail,
                Name: _testName,
                Surname: _testSurname,
                Password: _testPassword,
                Birthday: _testBirthday,
                UserRole: UserRoleEnum.Client
            );

            var existingUser = CreateTestUser();

            _mockUserDAO
                .Setup(dao => dao.GetUserByEmail(_testEmail))
                .ReturnsAsync(existingUser);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                 await _userService.CreateUserAsync(request));

            Assert.That(ex.Message, Does.Contain($"User with email {_testEmail} already exists"));
        }

        [Test]
        public void CreateInvalidUserAsync()
        {
            var invalidRequest = new UserRequest(
                Email: "",
                Name: _testName,
                Surname: _testSurname,
                Password: "",
                Birthday: _testBirthday,
                UserRole: UserRoleEnum.Client
            );

            _mockPasswordService
                .Setup(s => s.CreatePassword(""))
                .Throws(new ArgumentException("Password cannot be empty"));

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await _userService.CreateUserAsync(invalidRequest));

            Assert.That(ex.Message, Does.Contain("Password cannot be empty"));
        }

        [Test]
        public async Task DeleteUserByIdAsync()
        {
            _mockUserDAO
                .Setup(dao => dao.DeleteUser(_testUserId))
                .Returns(Task.CompletedTask);

            await _userService.DeleteUserByIdAsync(_testUserId);

            _mockUserDAO.Verify(dao => dao.DeleteUser(_testUserId), Times.Once);
        }

        [Test]
        public void InvalidDeleteUserByIdAsync()
        {
            var wrongId = Guid.NewGuid();

            _mockUserDAO
                .Setup(dao => dao.DeleteUser(wrongId))
                .ThrowsAsync(new KeyNotFoundException($"User with id {wrongId} not found"));

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _userService.DeleteUserByIdAsync(wrongId));

            Assert.That(ex.Message, Does.Contain(wrongId.ToString()));
        }

        [Test]
        public async Task UpdateUserAsync()
        {
            var currentUser = CreateTestUser();
            var updatedEmail = "updated@example.com";
            var updatedName = "UpdatedName";
            var updatedRole = UserRoleEnum.Admin;

            var request = new UserRequest(
                Email: _testEmail,
                Name: updatedName,
                Surname: currentUser.GetSurname(),
                Password: _testPassword,
                Birthday: currentUser.GetBirthday(),
                UserRole: updatedRole
            );

            _mockUserDAO
                .Setup(dao => dao.GetUserByEmail(_testEmail))
                .ReturnsAsync(currentUser);

            _mockUserDAO
                .Setup(dao => dao.UpdateUser(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            await _userService.UpdateUserAsync(request);

            _mockUserDAO.Verify(dao => dao.UpdateUser(It.Is<User>(u =>
                u.GetEmail() == _testEmail &&
                u.GetName() == updatedName &&
                u.GetUserRole() == updatedRole)), Times.Once);
        }

        [Test]
        public void InvalidUpdateUserAsync()
        {
            var request = new UserRequest(
                Email: "nonexistent@example.com",
                Name: _testName,
                Surname: _testSurname,
                Password: _testPassword,
                Birthday: _testBirthday,
                UserRole: UserRoleEnum.Client
            );

            _mockUserDAO
                .Setup(dao => dao.GetUserByEmail("nonexistent@example.com"))
                .ThrowsAsync(new KeyNotFoundException("User not found"));

            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _userService.UpdateUserAsync(request));

            Assert.That(ex.Message, Does.Contain("User not found"));
        }

        [Test]
        [TestCase("", "John", "Doe", "password123", "1990-01-01", "Email cannot be null or empty")]
        [TestCase("test@example.com", "", "Doe", "password123", "1990-01-01", "Name cannot be null or empty")]
        [TestCase("test@example.com", "John", "", "password123", "1990-01-01", "Surname cannot be null or empty")]
        [TestCase("test@example.com", "John", "Doe", "password123", "2050-01-01", "Birthday cannot be in the future")]
        public void UpdateInvalidUserAsync_Parameterized(
            string email,
            string name,
            string surname,
            string password,
            string birthdayStr,
            string expectedErrorMessage)
        {
            var currentUser = CreateTestUser();
            var birthday = DateOnly.Parse(birthdayStr);

            var invalidRequest = new UserRequest(
                Email: email,
                Name: name,
                Surname: surname,
                Password: password,
                Birthday: birthday,
                UserRole: UserRoleEnum.Client
            );

            _mockUserDAO
                .Setup(dao => dao.GetUserByEmail(_testEmail))
                .ReturnsAsync(currentUser);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await _userService.UpdateUserAsync(invalidRequest));

            Assert.That(ex.Message, Does.Contain(expectedErrorMessage));
            _mockUserDAO.Verify(dao => dao.UpdateUser(It.IsAny<User>()), Times.Never);
        }
    }
}