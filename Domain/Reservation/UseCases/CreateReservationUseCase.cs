using Domain.Error;

namespace Domain.Reservation.UseCases
{
    internal class CreateReservationUseCase
    {
    }

    public class Response
    {
        public class Fail(string message) : RoomAsyncError(message)
        {
            public class UnauthorizedAccess() : Fail(string.Empty);

            public class InvalidReservationHolder() : Fail("Invalid reservation holder.");

            public class InvalidRoom() : Fail("Room is not able to book or make a reservation for.");

            public class InvalidDateRange() : Fail("Invalid date range for reservation. Start date must be before end date.");

            public class RoomAlreadyReserved() : Fail("Room is already reserved for the given date range.");
        }

        public class Success
        {
            public Success(ReservationId reservationId) => ReservationId = reservationId;
            public ReservationId ReservationId { get; set; }
        }
    }
}
