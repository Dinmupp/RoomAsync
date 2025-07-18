using Domain.Error;
using Domain.Reservation;
using Domain.TableReservation.Driven;
using Domain.TableReservation.Request;

namespace Domain.TableReservation.UseCase
{
    public class CreateTableReservationUseCase
    {
        private readonly ITableReservationRepository _repository;

        public CreateTableReservationUseCase(ITableReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Response.Success, Response.Fail>> ExecuteAsync(
            CreateTableReservationRequest request,
            CancellationToken cancellationToken = default)
        {
            return await _repository.AddTableReservationAsync(request, cancellationToken);
        }

        public class Response
        {
            public class Fail(string message) : RoomAsyncError(message)
            {
            }

            public class Success
            {
                public Success(ReservationId reservationId) => ReservationId = reservationId;
                public ReservationId ReservationId { get; set; }
            }
        }
    }
}
