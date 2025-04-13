using Application.User;
using Domain;
using Domain.Infrastructure;
using Domain.Session.Driven;
using Domain.User.Driven;
using Domain.User.Driver;
using Domain.User.UseCases;
using Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompositionRoot
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserDriverPort, UserDriverImplementation>();

            services.AddScoped<CreateUserUseCase>();


            services.AddSingleton<UserContext>();
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString, string loggingDb)
        {
#if NET9_0
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

            // Register LoggingDbContext
            services.AddDbContext<LoggingDbContext>(options =>
                options.UseSqlServer(loggingDb));
#endif
            return services;
        }

        public static IServiceCollection AddInMemoryDatabase(this IServiceCollection services, string connectionString, string loggingDb)
        {
#if NET9_0
            services.AddDbContext<ApplicationDbContext>(options =>
                                options.UseInMemoryDatabase("InMemoryDb"));

            services.AddDbContext<LoggingDbContext>(options =>
                options.UseInMemoryDatabase("LoggingInMemoryDb"));
#endif
            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
#if NET9_0
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
#endif
            return services;
        }

        public static IServiceCollection AddLogger(this IServiceCollection services)
        {
#if NET9_0
            services.AddScoped<ILoggerService, LoggerService>();
#endif
            return services;
        }

        public static IServiceCollection AddOAuth(this IServiceCollection services, IConfigurationSection oAuthConfig)
        {
#if NET9_0
            services.Configure<OAuthConfig>(oAuthConfig);
            services.AddScoped<IOAuthAdapter, OAuthAdapter.OAuthAdapter>();
            services.AddHttpClient<IOAuthAdapter, OAuthAdapter.OAuthAdapter>();
            services.AddScoped<LoginUseCase>();
#endif

            return services;
        }

        public static IServiceCollection AddPrometheusExporter(this IServiceCollection services)
        {
#if NET9_0
            services.AddPrometheusExporter();
#endif

            return services;
        }
    }
}
