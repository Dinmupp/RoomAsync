using Domain.Infrastructure.Rooms;
using Domain.Room.Driven;
using Domain.Room.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CompositionRoot.Room
{
    public static class RoomDependency
    {
        public static void AddRoomDriver(IServiceCollection services)
        {
            services.TryAddTransient<FindAvailableRoomsUseCase>();
        }

        public static void AddRoomDriven(IServiceCollection services)
        {
            services.TryAddTransient<IRoomRepository, RoomRepository>();
        }
    }
}
