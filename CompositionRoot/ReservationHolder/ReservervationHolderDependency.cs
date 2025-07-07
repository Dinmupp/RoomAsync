using Domain.Infrastructure.ReservationHolder;
using Domain.Infrastructure.ReservationHolder.Commands;
using Domain.ReservationHolder.Driven;
using Domain.ReservationHolder.Driven.Commands;
using Domain.ReservationHolder.Driver;
using Domain.ReservationHolder.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CompositionRoot.Reserveration
{
    public static class ReservationHolderDependency
    {
        public static void AddReservationHolderDriver(IServiceCollection services)
        {
            services.TryAddTransient<IReservervationHolderDriverPort, ReservationHolderDriverImplementation>();
            services.TryAddTransient<CreateReservationHolderUseCase>();
            services.TryAddTransient<FindReservationHolderUseCase>();
            services.TryAddTransient<SelfCheckInUseCase>();
            services.TryAddTransient<SelfCheckOutUseCase>();
        }

        public static void AddReservationHolderDriven(IServiceCollection services)
        {
            services.TryAddTransient<IReservationHolderRepository, ReservationHolderRepository>();
            services.TryAddTransient<ISelfCheckInCommandHandler, SelfCheckInCommandHandler>();
            services.TryAddTransient<ISelfCheckOutCommandHandler, SelfCheckOutCommandHandler>();
        }
    }
}
