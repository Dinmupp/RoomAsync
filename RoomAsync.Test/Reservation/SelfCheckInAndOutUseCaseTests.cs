using Domain;
using Domain.ContactWay;
using Domain.Reservation;
using Domain.Reservation.Driver;
using Domain.ReservationHolder.Driver;
using Domain.ReservationHolder.Request;

namespace RoomAsync.Test.Reservation
{
    public class SelfCheckInAndOutUseCaseTests : BaseSeedTestData
    {
        private readonly ILoggerService _loggerService;
        protected readonly IReservervationHolderDriverPort ReservationHolderDriver;
        protected readonly IReservationDriverPort ReservationDriver;
        public SelfCheckInAndOutUseCaseTests(TestFixture testFixture) : base(testFixture)
        {
            // Resolve the UserDriverPort
            ReservationHolderDriver = Scope.ServiceProvider.GetRequiredService<IReservervationHolderDriverPort>();
            ReservationDriver = Scope.ServiceProvider.GetRequiredService<IReservationDriverPort>();
            _loggerService = Scope.ServiceProvider.GetRequiredService<ILoggerService>();
        }

        [Fact]
        public async Task SelfCheckIn_Integration_SetsCheckedInAt()
        {
            //Given 
            var reserverationId = await CreateNewReservation();


            var reserveration = await ReservationDriver.GetAsync(reserverationId, CancellationToken.None);

            //Act
            var result = await ReservationHolderDriver.SelfCheckIn(new SelfCheckInRequest(reserveration.Code), CancellationToken.None);

            result.TryGetValue(out var response);

            Assert.NotNull(response);
            Assert.True(response.ReservationHolderId.HasValue);

            reserveration = await ReservationDriver.GetAsync(reserverationId, CancellationToken.None);

            Assert.True(reserveration.CheckedInAt!.Value.Day == DateTime.UtcNow.Day);
        }

        [Fact]
        public async Task SelfCheckOut_Integration_SetsCheckedOutAt()
        {
            //Given 
            var reserverationId = await CreateNewReservation();


            var reserveration = await ReservationDriver.GetAsync(reserverationId, CancellationToken.None);

            //Act
            var result = await ReservationHolderDriver.SelfCheckOut(new SelfCheckOutRequest(reserveration.Code), CancellationToken.None);

            result.TryGetValue(out var response);

            Assert.NotNull(response);
            Assert.True(response.ReservationHolderId.HasValue);
            Assert.True(reserveration.CheckedOutAt!.Value.Day == DateTime.UtcNow.Day);
        }

        private async Task<ReservationId> CreateNewReservation()
        {
            //Given
            //Ensure Rooms Exist int the Database
            await SeedAvaibleRoomAsync(Domain.Room.RoomType.Standard, "Standard", 1, 101, CancellationToken.None);


            // Act
            var result = await ReservationDriver.AddReservationAsync(new Domain.Reservation.Request.CreateReservationRequest
            {
                ReservationHolderName = "Test Holder",
                RoomType = Domain.Room.RoomType.Standard,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(2),
                ReservationHolderEmail = new Email("Test@gmail.com"),
                ReservationHolderPhone = new Phone(CountryCode.Sweden, "730859054"),
                ReservationHolderId = "8209163974"
            }, CancellationToken.None);

            result.TryGetValue(out var reservation);

            Assert.NotNull(reservation);
            Assert.True(reservation.ReservationId.HasValue);

            return reservation.ReservationId.Value;
        }
    }
}
