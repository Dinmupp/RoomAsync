using Domain;
using Domain.Session;
using Domain.User.UseCases;

namespace RoomAsync.Test
{
    public class LoginUseCaseTests : IClassFixture<TestFixture>
    {
        private readonly IOAuthAdapter _oAuthAdapter;
        private readonly LoginUseCase _loginUseCase;

        public LoginUseCaseTests(TestFixture fixture)
        {
            _oAuthAdapter = fixture.ServiceProvider.GetRequiredService<IOAuthAdapter>();
            _loginUseCase = new LoginUseCase(_oAuthAdapter);
        }

        [Fact]
        public async Task Login_ValidCredentials_ShouldReturnSessionId()
        {
            // Arrange
            var username = "Admin";
            var password = "Admin";
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

            await _oAuthAdapter.AuthenticateAsync(username, password);
            await _oAuthAdapter.CreateSessionAsync("access-token");


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
