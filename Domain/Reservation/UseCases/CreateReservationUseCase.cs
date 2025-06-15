using Domain.Error;
using Domain.Reservation.Driven;
using Domain.Reservation.Request;
using Domain.Room.UseCase;

namespace Domain.Reservation.UseCases
{
    public sealed class CreateReservationUseCase
    {
        private readonly IReservationRepository _repository;
        private readonly FindAvailableRoomsUseCase _findAvailableRoomsUseCase;
        public CreateReservationUseCase(IReservationRepository repository, FindAvailableRoomsUseCase findAvailableRoomsUseCase)
        {
            _repository = repository;
            _findAvailableRoomsUseCase = findAvailableRoomsUseCase;
        }

        public async Task<Result<Response.Success, Response.Fail>> Execute(CreateReservationRequest request, CancellationToken cancellation = default)
        {
            var room = await _findAvailableRoomsUseCase.Execute(new Room.Requests.FindAvailableRoomsRequest { RoomType = request.RoomType }, cancellation);

            if (room.TryGetError(out var error))
            {
                return error switch
                {
                    FindAvailableRoomsUseCase.Response.Fail.NoAvailableRooms => new Response.Fail.RoomAlreadyReserved(),
                    _ => throw new ArgumentOutOfRangeException(nameof(error), error, null)
                };
            }

            room.TryGetValue(out var roomEntity);

            var result = await _repository.AddReservationAsync(request, roomEntity.Rooms.First(), cancellation);

            return result;
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
}
