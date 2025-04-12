using Domain;
using Domain.Infrastructure;
using Domain.Session.Driven;
using Domain.User.Driven;
using Domain.User.UseCases;
using Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prometheus.Middleware;

namespace CompositionRoot
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateUserUseCase>();
            services.AddScoped<LoginUseCase>();

            services.AddSingleton<UserContext>();
            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, string loggingDb, string targetFramework)
        {
            if (targetFramework == ".NET9")
            {
                // Use EF Core for .NET 9
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

                // Register LoggingDbContext
                services.AddDbContext<LoggingDbContext>(options =>
                    options.UseSqlServer(loggingDb));

                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<ISessionRepository, SessionRepository>();
            }

            return services;
        }

        public static IServiceCollection AddOAuth(this IServiceCollection services, string targetFramework)
        {
            if (targetFramework == ".NET9")
            {
                services.AddScoped<IOAuthAdapter, OAuthAdapter.OAuthAdapter>();
            }

            return services;
        }

        public static IServiceCollection AddPrometheusExporter(this IServiceCollection services, string targetFramework)
        {
            if (targetFramework == ".NET9")
            {
                services.AddPrometheusExporter();
            }

            return services;
        }
    }
}
