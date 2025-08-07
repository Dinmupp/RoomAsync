using Domain;
using Domain.Room.Driver;
using Domain.Room.Specifications;

namespace RoomAsync.Test.Room
{
    public class GetAllRoomTest : BaseSeedTestData
    {
        private readonly ILoggerService _loggerService;
        protected readonly IRoomDriverPort _roomDriverPort;
        private readonly RoomSpecificationFactory _roomSpecificationFactory;
        public GetAllRoomTest(TestFixture testFixture) : base(testFixture)
        {
            // Resolve the UserDriverPort
            _roomDriverPort = Scope.ServiceProvider.GetRequiredService<IRoomDriverPort>();
            _loggerService = Scope.ServiceProvider.GetRequiredService<ILoggerService>();
            _roomSpecificationFactory = Scope.ServiceProvider.GetRequiredService<RoomSpecificationFactory>(); ;
        }


        [Fact]
        public async Task GetAllRoomsShould()
        {
            _loggerService.SetCorrelationId(Guid.NewGuid().ToString());
            _loggerService.LogInformation("Test started");

            //Given
            //Ensure Rooms Exist int the Database
            await SeedAvaibleRoomAsync(Domain.Room.RoomType.Standard, "Standard", 1, 101, CancellationToken.None);
            await SeedAvaibleRoomAsync(Domain.Room.RoomType.Single, "Single", 2, 201, CancellationToken.None);
            await SeedAvaibleRoomAsync(Domain.Room.RoomType.Double, "Double", 3, 301, CancellationToken.None);
            await SeedAvaibleRoomAsync(Domain.Room.RoomType.Deluxe, "Deluxe", 4, 401, CancellationToken.None);


            // Act
            var result = await _roomDriverPort.FindAsync(_roomSpecificationFactory.CreateGetAllSpecification(null, "", "", false), CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.TotalCount == 4, "Expected 4 found " + result.TotalCount);
        }

        [Fact]
        public async Task GetAllRoomsWithRoomNumberFilterShould()
        {
            _loggerService.SetCorrelationId(Guid.NewGuid().ToString());
            _loggerService.LogInformation("Test started");

            //Given
            //Ensure Rooms Exist int the Database
            await SeedAvaibleRoomAsync(Domain.Room.RoomType.Standard, "Standard", 1, 101, CancellationToken.None);
            await SeedAvaibleRoomAsync(Domain.Room.RoomType.Single, "Single", 2, 201, CancellationToken.None);
            await SeedAvaibleRoomAsync(Domain.Room.RoomType.Double, "Double", 3, 301, CancellationToken.None);
            await SeedAvaibleRoomAsync(Domain.Room.RoomType.Deluxe, "Deluxe", 4, 401, CancellationToken.None);


            // Act
            var result = await _roomDriverPort.FindAsync(_roomSpecificationFactory.CreateGetAllSpecification(null, "20", "", false), CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Rooms.Count == 1, "Expected 1 found " + result.Rooms.Count);
            Assert.True(result.TotalCount == 4, "Expected 4 found " + result.TotalCount);
        }
    }
}
