using Domain.Session;
using Domain.User.Driver;
using Testcontainers.Keycloak;

namespace RoomAsync.Test
{
    public class LoginUseCaseTests : IClassFixture<TestFixture>, IAsyncLifetime
    {
        private readonly IUserDriverPort _userDriver;
        private readonly KeycloakContainer _keycloakContainer;

        public LoginUseCaseTests(TestFixture testFixture)
        {
            _keycloakContainer = new KeycloakBuilder()
               .WithPortBinding(8080, true)
               .WithPassword("admin")
               .WithUsername("admin")
               .Build();
            _userDriver = testFixture.ServiceProvider.GetRequiredService<IUserDriverPort>();
        }

        public async ValueTask DisposeAsync() =>
            await _keycloakContainer.DisposeAsync();

        public async ValueTask InitializeAsync() =>
            await _keycloakContainer.StartAsync();

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

            var accessToken = await _userDriver.LoginUserAsync(username, password, CancellationToken.None);

            // Assert
            Assert.Equal(expectedSessionId, accessToken);
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
