using Domain;
using Domain.Infrastructure;
using Domain.Infrastructure.Reservations;
using Domain.Reservation.Driven;
using Domain.Reservation.Driver;
using Domain.Reservation.UseCases;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CompositionRoot.Reserveration
{
    public static class ReservationDependency
    {
        public static void AddReservationDriver(IServiceCollection services)
        {
            services.TryAddTransient<IReservationDriverPort, ReservationDriverImplementation>();
            services.TryAddTransient<CreateReservationUseCase>();
        }

        public static void AddReservationDriven(IServiceCollection services)
        {
            services.TryAddTransient<IReservationRepository, ReservationRepository>();
            services.TryAddTransient<ICodeGeneratorService, CodeGeneratorService>();
        }
    }
}
