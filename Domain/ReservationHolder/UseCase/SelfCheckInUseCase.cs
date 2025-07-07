using Domain.Error;
using Domain.ReservationHolder.Driven.Commands;
using Domain.ReservationHolder.Request;

namespace Domain.ReservationHolder.UseCase
{
    public sealed class SelfCheckInUseCase
    {
        private readonly ISelfCheckInCommandHandler _command;
        public SelfCheckInUseCase(ISelfCheckInCommandHandler command)
        {
            _command = command;
        }

        public async Task<Result<Response.Success, Response.Fail>> Execute(SelfCheckInRequest request, CancellationToken cancellation = default)
        {
            return await _command.Command(request, cancellation);
        }

        public static class Response
        {
            public class Success
            {
                public ReservationHolderId ReservationHolderId { get; }
                public Success(ReservationHolderId reservationHolderId) => ReservationHolderId = reservationHolderId;
            }
            public abstract class Fail(string message) : RoomAsyncError(message)
            {
                public class NotFound() : Fail("Reservation holder not found.");
                public class Error(string message) : Fail(message);
            }
        }
    }
}
