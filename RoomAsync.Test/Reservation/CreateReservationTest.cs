using Domain;
using Domain.ContactWay;
using Domain.Notification.Driver;
using Domain.Notification.Request;
using Domain.Reservation.Driver;

namespace RoomAsync.Test.Reservation
{
    public class CreateReservationTest : BaseSeedTestData
    {
        private readonly ILoggerService _loggerService;
        protected readonly IReservationDriverPort ReservationDriver;
        protected readonly INotificationDriverPort NotificationDriver;
        public CreateReservationTest(TestFixture testFixture) : base(testFixture)
        {
            // Resolve the UserDriverPort
            ReservationDriver = Scope.ServiceProvider.GetRequiredService<IReservationDriverPort>();
            _loggerService = Scope.ServiceProvider.GetRequiredService<ILoggerService>();
            NotificationDriver = Scope.ServiceProvider.GetRequiredService<INotificationDriverPort>();
        }

        [Fact]
        public async Task CreateReservationShould()
        {
            _loggerService.SetCorrelationId(Guid.NewGuid().ToString());
            _loggerService.LogInformation("Test started");

            await CreateNewReservation();
        }

        private async Task CreateNewReservation()
        {
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
                ReservationHolderEmail = new Email("Test@gmail.com"),
                ReservationHolderPhone = new Phone("46", "730859054"),
                ReservationHolderId = "8209163974"
            }, CancellationToken.None);

            result.TryGetValue(out var reservation);

            Assert.NotNull(reservation);
            Assert.True(reservation.ReservationId.HasValue);
        }

#if DEBUG
        [Fact(Skip = "Runs only on demand or scheduled. Set RUN_SMS_TEST=true to enable.")]
#else
        [Fact(Skip = "Runs only on demand or scheduled. Set RUN_SMS_TEST=true to enable.")]
#endif
        public async Task CreateReservationAndSendSmsShould()
        {
            _loggerService.SetCorrelationId(Guid.NewGuid().ToString());
            _loggerService.LogInformation("Test started");

            await CreateNewReservation();
            var result = await NotificationDriver.SendSms(new SmsRequest(
                new Phone("46", "730859052"),
                "Your reservation has been created successfully. - RoomAsync Test")
                );

            if (result.TryGetError(out var error))
            {
                Assert.Throws<ArgumentException>(() => error.Message);
            }
        }


#if DEBUG
        [Fact(Skip = "Runs only on demand or scheduled. Set RUN_SMS_TEST=true to enable.")]
#else
                [Fact(Skip = "Runs only on demand or scheduled. Set RUN_SMS_TEST=true to enable.")]
#endif
        //[Fact]
        public async Task CreateReservationAndSendEmailShould()
        {
            _loggerService.SetCorrelationId(Guid.NewGuid().ToString());
            _loggerService.LogInformation("Test started");

            await CreateNewReservation();
            var result = await NotificationDriver.SendEmail(new EmailRequest(
                new Email("christoffer.l@hotmail.com"), "Test", "Testing email reservation confirmation from RoomAsync - Test")
                );

            if (result.TryGetError(out var error))
            {
                Assert.Throws<ArgumentException>(() => error.Message);
            }
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
                ReservationHolderEmail = new Email("Test@gmail.com"),
                ReservationHolderPhone = new Phone("46", "730859054"),
                ReservationHolderId = "8209163974"
            }, CancellationToken.None);

            result.TryGetValue(out var reservation);

            Assert.Null(reservation);

            result.TryGetError(out var error);

            Assert.True(error is Domain.Reservation.UseCases.CreateReservationUseCase.Response.Fail.RoomAlreadyReserved, "Expected RoomAlreadyReserved error but got: " + error.GetType().Name);
        }
    }
}
