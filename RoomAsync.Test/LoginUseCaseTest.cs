using Domain.Session;
using Domain.User.Driver;
using Testcontainers.Keycloak;

namespace RoomAsync.Test
{
    public class LoginUseCaseTests : IAsyncLifetime, IAsyncDisposable
    {
        readonly AsyncServiceScope _scope;
        private readonly IUserDriverPort _userDriver;
        private readonly KeycloakContainer _keycloakContainer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testFixture"></param>
        public LoginUseCaseTests(TestFixture testFixture)
        {
            _scope = testFixture.CreateScopeAsync();
            _keycloakContainer = new KeycloakBuilder()
                .WithImage("keycloak/keycloak:26.0")
                .WithExposedPort(8081)
                .WithPortBinding(8081, 8080)
                .WithEnvironment("KEYCLOAK_IMPORT", "/opt/keycloak/data/import/import.json")
                .WithResourceMapping("./Import/import.json", "/opt/keycloak/data/import")
                .WithCommand("--import-realm")
                .Build();
            _userDriver = _scope.ServiceProvider.GetRequiredService<IUserDriverPort>();
        }

        public async ValueTask DisposeAsync()
        {
            await _scope.DisposeAsync();
            await _keycloakContainer.DisposeAsync();
        }

        public async ValueTask InitializeAsync() =>
            await _keycloakContainer.StartAsync();

        [Fact]
        public async Task Login_ValidCredentials_ShouldReturnSessionId()
        {
            // Arrange
            var username = "myuser";
            var password = "mypassword";

            var accessToken = await _userDriver.LoginUserAsync(username, password, CancellationToken.None);

            // Assert
            Assert.NotNull(accessToken);
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
