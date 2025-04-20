using Domain.User.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RoomAsync.Test;

[assembly: AssemblyFixture(typeof(TestFixture))]
namespace RoomAsync.Test
{
    public class TestFixture : IDisposable
    {
        readonly IHost _app;

        public AsyncServiceScope CreateScopeAsync() => _app.Services.CreateAsyncScope();
        public IServiceScope CreateScope() => _app.Services.CreateScope();

        public TestFixture()
        {
            var builder = Host.CreateDefaultBuilder();

            builder.UseDefaultServiceProvider(options =>
            {
                options.ValidateOnBuild = true;
                options.ValidateScopes = true;
            });

            builder.ConfigureHostConfiguration(configuration =>
            {
                configuration
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            });

            builder.ConfigureServices((builderContext, services) =>
            {
                var startup = new Startup(builderContext.Configuration);
                startup.ConfigureServices(services);
                var p = services.BuildServiceProvider();
                var a = p.GetRequiredService<CreateUserUseCase>();

            });
            _app = builder.Build();
        }

        public void Dispose()
        {
            _app.Dispose();
        }
    }
}
