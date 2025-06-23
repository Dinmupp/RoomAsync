using Domain.Error;
using Domain.Reservation.Driven;
using Domain.Reservation.Request;
using Domain.ReservationHolder;
using Domain.ReservationHolder.Request;
using Domain.ReservationHolder.Specifications;
using Domain.ReservationHolder.UseCase;
using Domain.Room.UseCase;

namespace Domain.Reservation.UseCases
{
    public sealed class CreateReservationUseCase
    {
        private readonly IReservationRepository _repository;
        private readonly FindAvailableRoomsUseCase _findAvailableRoomsUseCase;
        private readonly FindReservationHolderUseCase _findReservationHolderUseCase;
        private readonly CreateReservationHolderUseCase _createReservationHolderUseCase;
        public CreateReservationUseCase(IReservationRepository repository, FindAvailableRoomsUseCase findAvailableRoomsUseCase, FindReservationHolderUseCase findReservationHolderUseCase, CreateReservationHolderUseCase createReservationHolderUseCase)
        {
            _repository = repository;
            _findAvailableRoomsUseCase = findAvailableRoomsUseCase;
            _findReservationHolderUseCase = findReservationHolderUseCase;
            _createReservationHolderUseCase = createReservationHolderUseCase;
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

            ReservationHolderId reservationHolderIdrequest = "";
            var reservationHolder = await _findReservationHolderUseCase.Execute(new GetByNameAndPhoneAndEmail(request.ReservationHolderEmail, request.ReservationHolderPhone, request.ReservationHolderName), cancellation);

            if (reservationHolder.TryGetError(out var reservationHolderError))
            {
                if (reservationHolderError is FindReservationHolderUseCase.Response.Fail.DidNotFindReservationHolder)
                {
                    var createResult = await _createReservationHolderUseCase.Execute(new CreateReservationHolderRequest(
                        request.ReservationHolderName,
                        request.ReservationHolderEmail,
                        request.ReservationHolderPhone,
                        request.ReservationHolderId), cancellation);
                    if (createResult.TryGetError(out var createError))
                    {
                        return new Response.Fail.InvalidReservationHolder();
                    }
                    createResult.TryGetValue(out var reservationHolderId);
                    reservationHolderIdrequest = reservationHolderId.ReservationHolder;
                }
            }

            reservationHolder.TryGetValue(out var reservationsHolders);

            if (reservationsHolders is not null && !reservationHolderIdrequest.HasValue && reservationsHolders.ReservationHolders.Any())
            {
                reservationHolderIdrequest = reservationsHolders.ReservationHolders.First();
            }

            var result = await _repository.AddReservationAsync(request, roomEntity.Rooms.First(), reservationHolderIdrequest, cancellation);

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
