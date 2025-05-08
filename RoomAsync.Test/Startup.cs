using CompositionRoot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RoomAsync.Test
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add logging
            services.AddLogging(config =>
            {
                config.AddConsole();
                config.AddDebug();
            });

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var loggingDb = Configuration.GetConnectionString("LoggingDatabase");

            //builder.Services.AddPrometheusExporter(".NET9");
            services.AddInfrastructure();
            services.AddApplication();
            services.AddInMemoryDatabase(connectionString!, loggingDb!);
            services.AddOAuth(Configuration.GetSection("OAuthConfig"));
            services.AddLogger(Configuration);
        }
    }
}
