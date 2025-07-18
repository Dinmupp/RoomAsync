using Domain;
using Domain.Allergy;
using Domain.ReservationHolder;
using Domain.TableReservation.Driver;
using Domain.TableReservation.Request;

namespace RoomAsync.Test.TableReservation
{
    public class TableReservationIntegrationTests : BaseTest
    {
        private readonly ILoggerService _loggerService;
        protected readonly ITableReservationDriverPort _tableDriverPort;
        public TableReservationIntegrationTests(TestFixture testFixture) : base(testFixture)
        {
            // Resolve the UserDriverPort
            _loggerService = Scope.ServiceProvider.GetRequiredService<ILoggerService>();
            _tableDriverPort = Scope.ServiceProvider.GetRequiredService<ITableReservationDriverPort>();
        }


        [Fact]
        public async Task Can_Create_And_Get_TableReservation()
        {
            // Arrange

            var request = new CreateTableReservationRequest
            {
                StartDate = DateTimeOffset.Now.AddHours(1),
                EndDate = DateTimeOffset.Now.AddHours(2),
                ReservationHolderId = new ReservationHolderId("holder-1"),
                NumberOfGuests = 4,
                Allergies = "peanut,shellfish",
                BillToRoomNumber = null,
                PaidAtCashier = false
            };

            // Act
            var result = await _tableDriverPort.CreateTableReservationAsync(request, CancellationToken.None);

            result.TryGetValue(out var tableReservation);
            // Assert
            var reservationId = tableReservation.ReservationId;

            var fetched = await _tableDriverPort.GetAsync(reservationId, CancellationToken.None);
            Assert.Equal(request.NumberOfGuests, fetched.NumberOfGuests);

            Assert.Contains(fetched.Allergies, x => x is PeanutAllergy);
            Assert.Contains(fetched.Allergies, x => x is ShellfishAllergy);
            Assert.Equal(request.ReservationHolderId, fetched.ReservationHolderId);
        }
    }
}
