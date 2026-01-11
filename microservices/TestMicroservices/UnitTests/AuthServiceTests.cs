using Moq;
using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Interfaces.Services;
using microservices.UserAPI.Domain.Services;
using microservices.UserAPI.API.Contracts.Requests;
using microservices.UserAPI.Domain.Models;
using microservices.UserAPI.Domain.Enums;

namespace TestMicroservices
{
    [TestFixture]
    public class AuthServiceTests
    {
        private Mock<IUserDAO> _mockUserDAO;
        private Mock<IPasswordDAO> _mockPasswordDAO;
        private Mock<IRefreshTokenDAO> _mockRefreshTokenDAO;
        private Mock<IUserService> _mockUserService;
        private Mock<IPasswordService> _mockPasswordService;
        private Mock<IJwtService> _mockJwtService;
        private AuthService _authService;

        private Guid _testUserId;
        private Guid _testPasswordId;
        private Guid _testRefreshTokenId;
        private string _testEmail;
        private string _testName;
        private string _testSurname;
        private string _testPassword;
        private DateOnly _testBirthday;

        [SetUp]
        public void Setup()
        {
            _mockUserDAO = new Mock<IUserDAO>();
            _mockPasswordDAO = new Mock<IPasswordDAO>();
            _mockRefreshTokenDAO = new Mock<IRefreshTokenDAO>();
            _mockUserService = new Mock<IUserService>();
            _mockPasswordService = new Mock<IPasswordService>();
            _mockJwtService = new Mock<IJwtService>();

            _authService = new AuthService(
                _mockUserDAO.Object,
                _mockPasswordDAO.Object,
                _mockRefreshTokenDAO.Object,
                _mockUserService.Object,
                _mockPasswordService.Object,
                _mockJwtService.Object
            );

            _testUserId = Guid.NewGuid();
            _testPasswordId = Guid.NewGuid();
            _testRefreshTokenId = Guid.NewGuid();
            _testEmail = "test@example.com";
            _testName = "John";
            _testSurname = "Doe";
            _testPassword = "password123";
            _testBirthday = new DateOnly(1990, 1, 1);
        }

        [Test]
        public async Task SignUpSuccessful()
        {
            var request = new UserRequest(
                Email: _testEmail,
                Name: _testName,
                Surname: _testSurname,
                Password: _testPassword,
                Birthday: _testBirthday
            );

            _mockUserDAO
                .Setup(dao => dao.GetUsers())
                .ReturnsAsync(new List<User>());

            _mockUserService
                .Setup(s => s.CreateUserAsync(request))
                .ReturnsAsync(_testUserId);

            var existingUser = new User(
                _testUserId,
                _testEmail,
                _testName,
                _testSurname,
                _testBirthday,
                DateTime.UtcNow,
                _testPasswordId,
                null,
                UserRoleEnum.Client
            );

            _mockUserDAO
                .Setup(dao => dao.GetUserById(_testUserId))
                .ReturnsAsync(existingUser);

            _mockJwtService
                .Setup(s => s.GenerateAccessToken(It.IsAny<User>()))
                .Returns("access_token");

            _mockJwtService
                .Setup(s => s.GenerateRefreshToken())
                .Returns("refresh_token");

            _mockRefreshTokenDAO
                .Setup(dao => dao.CreateRefreshToken(It.IsAny<RefreshToken>()))
                .ReturnsAsync(_testRefreshTokenId);

            var result = await _authService.SignUp(request);

            Assert.That(result.UserId, Is.EqualTo(_testUserId));
            Assert.That(result.Email, Is.EqualTo(_testEmail));
            Assert.That(result.AccessToken, Is.EqualTo("access_token"));
            Assert.That(result.RefreshToken, Is.EqualTo("refresh_token"));
        }

        [Test]
        public void SignUpUnsuccessful()
        {
            var request1 = new UserRequest(
                Email: _testEmail,
                Name: _testName,
                Surname: _testSurname,
                Password: _testPassword,
                Birthday: _testBirthday
            );

            var existingUser = new User(
                _testUserId,
                _testEmail,
                _testName,
                _testSurname,
                _testBirthday,
                DateTime.UtcNow,
                _testPasswordId,
                null,
                UserRoleEnum.Client
            );

            _mockUserDAO
                .Setup(dao => dao.GetUsers())
                .ReturnsAsync(new List<User> { existingUser });

            var ex1 = Assert.ThrowsAsync<ArgumentException>(async () =>
                await _authService.SignUp(request1));

            Assert.That(ex1.Message, Does.Contain($"User with email '{_testEmail}' already exists"));

            var request2 = new UserRequest(
                Email: _testEmail,
                Name: _testName,
                Surname: _testSurname,
                Password: _testPassword,
                Birthday: _testBirthday
            );

            _mockUserDAO
                .Setup(dao => dao.GetUsers())
                .ReturnsAsync(new List<User>());

            _mockUserService
                .Setup(s => s.CreateUserAsync(request2))
                .ThrowsAsync(new Exception("Invalid email format"));

            var ex2 = Assert.ThrowsAsync<Exception>(async () =>
                await _authService.SignUp(request2));

            Assert.That(ex2.Message, Does.Contain("Registration failed"));
        }

        [Test]
        public async Task LogInSuccessful()
        {
            var request = new LoginRequest(
                Email: _testEmail,
                Password: _testPassword
            );

            var existingUser = new User(
                _testUserId,
                _testEmail,
                _testName,
                _testSurname,
                _testBirthday,
                DateTime.UtcNow,
                _testPasswordId,
                null,
                UserRoleEnum.Client
            );

            _mockUserDAO
                .Setup(dao => dao.GetUsers())
                .ReturnsAsync(new List<User> { existingUser });

            var password = new Password(
                _testPasswordId,
                new byte[64],
                new byte[128]
            );

            _mockPasswordDAO
                .Setup(dao => dao.GetPasswordById(_testPasswordId))
                .ReturnsAsync(password);

            _mockPasswordService
                .Setup(s => s.VerifyPassword(_testPassword, It.IsAny<byte[]>(), It.IsAny<byte[]>()))
                .Returns(true);

            _mockJwtService
                .Setup(s => s.GenerateAccessToken(It.IsAny<User>()))
                .Returns("access_token");

            _mockJwtService
                .Setup(s => s.GenerateRefreshToken())
                .Returns("refresh_token");

            _mockRefreshTokenDAO
                .Setup(dao => dao.CreateRefreshToken(It.IsAny<RefreshToken>()))
                .ReturnsAsync(_testRefreshTokenId);

            var result = await _authService.LogIn(request);

            Assert.That(result.UserId, Is.EqualTo(_testUserId));
            Assert.That(result.Email, Is.EqualTo(_testEmail));
            Assert.That(result.AccessToken, Is.EqualTo("access_token"));
            Assert.That(result.RefreshToken, Is.EqualTo("refresh_token"));
        }

        [Test]
        public void LogInUnsuccessful()
        {
            var request1 = new LoginRequest(
                Email: "nonexistent@example.com",
                Password: _testPassword
            );

            _mockUserDAO
                .Setup(dao => dao.GetUsers())
                .ReturnsAsync(new List<User>());

            var ex1 = Assert.ThrowsAsync<ArgumentException>(async () =>
                await _authService.LogIn(request1));

            Assert.That(ex1.Message, Does.Contain($"User with email '{request1.Email}' not found"));

            var request2 = new LoginRequest(
                Email: _testEmail,
                Password: "wrongpassword"
            );

            var existingUser = new User(
                _testUserId,
                _testEmail,
                _testName,
                _testSurname,
                _testBirthday,
                DateTime.UtcNow,
                _testPasswordId,
                null,
                UserRoleEnum.Client
            );

            _mockUserDAO
                .Setup(dao => dao.GetUsers())
                .ReturnsAsync(new List<User> { existingUser });

            var password = new Password(
                _testPasswordId,
                new byte[64],
                new byte[128]
            );

            _mockPasswordDAO
                .Setup(dao => dao.GetPasswordById(_testPasswordId))
                .ReturnsAsync(password);

            _mockPasswordService
                .Setup(s => s.VerifyPassword(request2.Password, It.IsAny<byte[]>(), It.IsAny<byte[]>()))
                .Returns(false);

            var ex2 = Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await _authService.LogIn(request2));

            Assert.That(ex2.Message, Is.EqualTo("Invalid password"));
        }

        [Test]
        public async Task LogOutSuccessful()
        {
            var existingUser = new User(
                _testUserId,
                _testEmail,
                _testName,
                _testSurname,
                _testBirthday,
                DateTime.UtcNow,
                _testPasswordId,
                _testRefreshTokenId,
                UserRoleEnum.Client
            );

            _mockUserDAO
                .Setup(dao => dao.GetUserById(_testUserId))
                .ReturnsAsync(existingUser);

            var result = await _authService.LogOut(_testUserId);

            Assert.That(result, Is.True);
            _mockUserDAO.Verify(dao => dao.UpdateUser(It.Is<User>(u => u.GetRefreshTokenId() == null)), Times.Once);
            _mockRefreshTokenDAO.Verify(dao => dao.DeleteRefreshToken(_testRefreshTokenId), Times.Once);
        }

        [Test]
        public void LogOutUnsuccessfulUserNotFound()
        {
            _mockUserDAO
                .Setup(dao => dao.GetUserById(_testUserId))
                .ThrowsAsync(new Exception("User not found"));

            var result = Assert.ThrowsAsync<Exception>(async () =>
                await _authService.LogOut(_testUserId));

            Assert.That(result.Message, Does.Contain("User not found"));
        }

        [Test]
        public async Task LogOutUnsuccessfulUserAlreadyLoggedOut()
        {
            var existingUser = new User(
                _testUserId,
                _testEmail,
                _testName,
                _testSurname,
                _testBirthday,
                DateTime.UtcNow,
                _testPasswordId,
                null,
                UserRoleEnum.Client
            );

            _mockUserDAO
                .Setup(dao => dao.GetUserById(_testUserId))
                .ReturnsAsync(existingUser);

            var result = await _authService.LogOut(_testUserId);

            Assert.That(result, Is.True);
            _mockUserDAO.Verify(dao => dao.UpdateUser(It.Is<User>(u => u.GetRefreshTokenId() == null)), Times.Once);
            _mockRefreshTokenDAO.Verify(dao => dao.DeleteRefreshToken(It.IsAny<Guid>()), Times.Never);
        }
    }
}