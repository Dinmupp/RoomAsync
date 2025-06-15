using Domain;
using Domain.Reservation.Driver;

namespace RoomAsync.Test.Reservation
{
    public class CreateReservationTest : BaseSeedTestData
    {
        private readonly ILoggerService _loggerService;
        protected readonly IReservationDriverPort ReservationDriver;
        public CreateReservationTest(TestFixture testFixture) : base(testFixture)
        {
            // Resolve the UserDriverPort
            ReservationDriver = Scope.ServiceProvider.GetRequiredService<IReservationDriverPort>();
            _loggerService = Scope.ServiceProvider.GetRequiredService<ILoggerService>();
        }

        [Fact]
        public async Task CreateReservationShould()
        {
            _loggerService.SetCorrelationId(Guid.NewGuid().ToString());
            _loggerService.LogInformation("Test started");

            //Given
            //Ensure Rooms Exist int the Database
            await SeedAvaibleRoomAsync(Domain.Room.RoomType.Standard, "Standard", 1, 101, CancellationToken.None);


            // Act
            var result = await ReservationDriver.AddReservationAsync(new Domain.Reservation.Request.CreateReservationRequest
            {
                ReservationHolderName = "Test Holder",
                RoomType = Domain.Room.RoomType.Standard,
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                ReservationHolderEmail = "Test@gmail.com",
                ReservationHolderPhone = "0730859054",
                ReservationHolderId = "8209163974"
            }, CancellationToken.None);

            result.TryGetValue(out var reservation);

            Assert.NotNull(reservation);
            Assert.True(reservation.ReservationId.HasValue);
        }

        [Fact]
        public async Task UnableToCreateReservationNoAvaibleRoomsShould()
        {
            _loggerService.SetCorrelationId(Guid.NewGuid().ToString());
            _loggerService.LogInformation("Test started");

            //Given
            //Ensure Rooms Exist int the Database
            await SeedAvaibleRoomAsync(Domain.Room.RoomType.Standard, "Standard", 1, 101, CancellationToken.None);


            // Act
            var result = await ReservationDriver.AddReservationAsync(new Domain.Reservation.Request.CreateReservationRequest
            {
                ReservationHolderName = "Test Holder",
                RoomType = Domain.Room.RoomType.Deluxe,
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                ReservationHolderEmail = "Test@gmail.com",
                ReservationHolderPhone = "0730859054",
                ReservationHolderId = "8209163974"
            }, CancellationToken.None);

            result.TryGetValue(out var reservation);

            Assert.Null(reservation);

            result.TryGetError(out var error);

            Assert.True(error is Domain.Reservation.UseCases.CreateReservationUseCase.Response.Fail.RoomAlreadyReserved, "Expected RoomAlreadyReserved error but got: " + error.GetType().Name);
        }
    }
}
