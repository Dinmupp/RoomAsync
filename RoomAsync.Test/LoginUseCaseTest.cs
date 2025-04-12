using Domain;
using Domain.Session;
using Domain.User.UseCases;
using NSubstitute;

namespace RoomAsync.Test
{
    public class LoginUseCaseTests
    {
        private readonly IOAuthAdapter _mockAdapter;
        private readonly LoginUseCase _loginUseCase;

        public LoginUseCaseTests()
        {
            _mockAdapter = Substitute.For<IOAuthAdapter>();
            _loginUseCase = new LoginUseCase(_mockAdapter);
        }

        [Fact]
        public async Task Login_ValidCredentials_ShouldReturnSessionId()
        {
            // Arrange
            var username = "testuser";
            var password = "password";
            var expectedSessionId = "session-id";

            var userInfo = new UserInfo
            {
                SessionId = expectedSessionId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                UserId = "userId",
                Username = username,
                Roles = "role1,role2",
                Claims = "claim1,claim2"
            };

            var _mockAdapter = Substitute.For<IOAuthAdapter>();

            _mockAdapter.AuthenticateAsync(username, password).Returns("access-token");
            _mockAdapter.CreateSessionAsync("access-token").Returns(new Session(userInfo));


            // Act
            var sessionId = await _loginUseCase.ExecuteAsync(username, password);

            // Assert
            Assert.Equal(expectedSessionId, sessionId);
        }
    }

    public class UserInfo : IUserSessionDataEntity
    {
        public string SessionId { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Roles { get; set; }
        public string? Claims { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
