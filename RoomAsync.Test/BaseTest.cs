using Domain;
using Domain.User.Driver;
using Testcontainers.Keycloak;

namespace RoomAsync.Test
{
    public abstract class BaseTest : IAsyncLifetime, IAsyncDisposable
    {
        protected readonly AsyncServiceScope Scope;
        protected readonly IUserDriverPort UserDriver;
        private static readonly KeycloakContainer SharedKeycloakContainer;
        private static bool IsContainerStarted = false;

        static BaseTest()
        {
            SharedKeycloakContainer = new KeycloakBuilder()
                .WithImage("keycloak/keycloak:latest")
                .WithExposedPort(8081)
                .WithPortBinding(8081, 8080)
                .WithEnvironment("KEYCLOAK_IMPORT", "/opt/keycloak/data/import/import.json")
                .WithResourceMapping("./Import/import.json", "/opt/keycloak/data/import")
                .WithCommand("--import-realm")
                .WithReuse(true)
                .Build();
        }

        protected BaseTest(TestFixture testFixture)
        {
            // Create a service scope
            Scope = testFixture.CreateScopeAsync();

            // Resolve the UserDriverPort
            UserDriver = Scope.ServiceProvider.GetRequiredService<IUserDriverPort>();
        }

        public async ValueTask DisposeAsync()
        {
            await Scope.DisposeAsync();
        }

        public async ValueTask InitializeAsync()
        {
            if (!IsContainerStarted)
            {
                await SharedKeycloakContainer.StartAsync();
                IsContainerStarted = true;
            }
        }

        protected async Task<UserContext> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            return await UserDriver.LoginUserAsync(username, password, cancellationToken);
        }
    }
}
