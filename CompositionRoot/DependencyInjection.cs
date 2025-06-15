using Application.User;
using Domain;
using Domain.Infrastructure;
using Domain.Infrastructure.Reservations;
using Domain.Infrastructure.Rooms;
using Domain.Infrastructure.Users;
using Domain.Reservation.Driven;
using Domain.Reservation.Driver;
using Domain.Reservation.UseCases;
using Domain.Room.Driven;
using Domain.Room.UseCase;
using Domain.Session.Driven;
using Domain.User.Driven;
using Domain.User.Driver;
using Domain.User.UseCases;
using KeyCloakOAuthAdapter;
using Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
namespace CompositionRoot
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.TryAddTransient<IUserDriverPort, UserDriverImplementation>();

            services.TryAddTransient<CreateUserUseCase>();

            services.TryAddTransient<IReservationDriverPort, ReservationDriverImplementation>();

            services.TryAddTransient<CreateReservationUseCase>();

            services.TryAddTransient<FindAvailableRoomsUseCase>();

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
                                options.UseInMemoryDatabase("InMemoryDb")
                                .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning)));


            services.AddDbContext<LoggingDbContext>(options =>
                options.UseInMemoryDatabase("LoggingInMemoryDb"));
#endif
            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
#if NET9_0
            services.TryAddTransient<IUserRepository, UserRepository>();
            services.TryAddTransient<ISessionRepository, SessionRepository>();
            services.TryAddTransient<IReservationRepository, ReservationRepository>();
            services.TryAddTransient<IRoomRepository, RoomRepository>();
#endif
            return services;
        }

        public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
        {
#if NET9_0
            services.AddScoped<ILoggerService, LoggerService>();
#endif

            // Check if Serilog should be used
            var useSerilog = configuration.GetValue<bool>("Logging:UseSerilog");

            if (useSerilog)
            {
                SetUpSerilog(services, configuration);
            }
            else
            {
                // Use the alternative LoggerService that logs directly to the database
                services.AddScoped<ILoggerService, LoggerService>();
            }
            return services;
        }

        private static void SetUpSerilog(IServiceCollection services, IConfiguration configuration)
        {
            var serilogConnectionString = configuration.GetConnectionString("LoggingDatabase");
            var serilogTableName = configuration.GetValue<string>("Logging:SerilogTableName") ?? "Logs";
            var sinkOptions = new MSSqlServerSinkOptions
            {
                TableName = serilogTableName,
                AutoCreateSqlTable = true
            };

            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                    {
                        new SqlColumn { ColumnName = "CorrelationId", DataType = SqlDbType.NVarChar, DataLength = 50 }
                    }
            };

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .WriteTo.MSSqlServer(
                    connectionString: serilogConnectionString,
                    sinkOptions: sinkOptions,
                    columnOptions: columnOptions,
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                )
                .CreateLogger();
            services.AddSingleton(Log.Logger);
            services.AddScoped<ILoggerService, SerilogLoggerService>();
        }

        public static IServiceCollection AddOAuth(this IServiceCollection services, IConfigurationSection oAuthConfig)
        {
#if NET9_0
            services.Configure<OAuthConfig>(oAuthConfig);
            services.AddScoped<IOAuthAdapter, OAuthKeycloakAdapter>();
            services.AddHttpClient<IOAuthAdapter, OAuthKeycloakAdapter>();
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
