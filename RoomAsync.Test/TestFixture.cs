using Microsoft.Extensions.Configuration;

namespace RoomAsync.Test
{
    public class TestFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; }

        public TestFixture()
        {
            // Build configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Set up the service collection
            var services = new ServiceCollection();

            // Initialize Startup and configure services
            var startup = new Startup(configuration);
            startup.ConfigureServices(services);

            // Build the service provider
            ServiceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
