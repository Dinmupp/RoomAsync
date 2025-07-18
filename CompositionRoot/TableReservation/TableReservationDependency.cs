using Domain.Infrastructure.TableReservations;
using Domain.TableReservation.Driven;
using Domain.TableReservation.Driver;
using Domain.TableReservation.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CompositionRoot.TableReservation
{
    public static class TableReservationDependency
    {
        public static void AddTableReservationDriver(IServiceCollection services)
        {
            services.TryAddTransient<ITableReservationDriverPort, TableReservationDriverImplementation>();
            services.TryAddTransient<CreateTableReservationUseCase>();
        }

        public static void AddTableReservationDriven(IServiceCollection services)
        {
            services.TryAddTransient<ITableReservationRepository, TableReservationRepository>();
        }
    }
}
