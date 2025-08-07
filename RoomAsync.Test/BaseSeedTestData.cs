using Domain;
using Domain.Infrastructure;
using Domain.Infrastructure.Rooms;
using Domain.Room;

namespace RoomAsync.Test
{
    public class BaseSeedTestData : IAsyncDisposable
    {
        protected readonly AsyncServiceScope Scope;
        protected readonly ApplicationDbContext DbContext;
        protected readonly ICodeGeneratorService codeGeneratorService;

        protected BaseSeedTestData(TestFixture testFixture)
        {
            // Create a service scope
            Scope = testFixture.CreateScopeAsync();

            DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            codeGeneratorService = Scope.ServiceProvider.GetRequiredService<ICodeGeneratorService>();
        }

        public async ValueTask DisposeAsync()
        {
            await Scope.DisposeAsync();
        }

        public async Task SeedAvaibleRoomAsync(RoomType roomType, string name, int floorLevel, int number, CancellationToken cancellationToken = default)
        {
            // Ensure the database is created
            await DbContext.Database.EnsureCreatedAsync(cancellationToken);
            DbContext.Rooms.AddRange(new List<RoomDataEntity>
            {
                new RoomDataEntity { RoomId = $"{floorLevel}-{number}",  RoomType = roomType, FloorLevel = floorLevel, RoomName = name, RoomNumber = number, Status = RoomStatus.Available  }
            });
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
