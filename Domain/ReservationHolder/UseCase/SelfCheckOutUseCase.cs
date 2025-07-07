using Domain.Error;
using Domain.ReservationHolder.Driven.Commands;
using Domain.ReservationHolder.Request;

namespace Domain.ReservationHolder.UseCase
{
    public sealed class SelfCheckOutUseCase
    {
        private readonly ISelfCheckOutCommandHandler _command;
        private readonly ILoggerService _logger;
        public SelfCheckOutUseCase(ISelfCheckOutCommandHandler command, ILoggerService logger)
        {
            _command = command;
            _logger = logger;
        }

        public async Task<Result<Response.Success, Response.Fail>> Execute(SelfCheckOutRequest request, CancellationToken cancellation = default)
        {
            _logger.LogInformation($"SelfCheckOut requested for Reservation Code: {request.Code}");

            var result = await _command.Command(request, cancellation);

            if (result.TryGetValue(out var success))
            {
                _logger.LogInformation($"SelfCheckOut succeeded for ReservationId: {success.ReservationHolderId}");
            }
            else if (result.TryGetError(out var error))
            {
                _logger.LogError($"SelfCheckOut failed for Reservation Code: {request.Code}. Reason: {error.Message}");
            }

            return result;
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
