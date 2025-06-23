using Domain.Error;
using Domain.ReservationHolder.Driven;
using Domain.ReservationHolder.Request;

namespace Domain.ReservationHolder.UseCase
{
    public class CreateReservationHolderUseCase
    {
        private readonly IReservationHolderRepository _repository;

        public CreateReservationHolderUseCase(IReservationHolderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Response.Success, Response.Fail>> Execute(CreateReservationHolderRequest request, CancellationToken cancellation = default)
        {
            var result = await _repository.Create(request, cancellation);

            return result;
        }

        public class Response
        {
            public class Fail(string message) : RoomAsyncError(message)
            {
                public class CreateReservationHolderError(string message) : Fail(message);

                public class UndefinedSpecification() : Fail("The specification is undefined or not supported.");
            }

            public class Success
            {
                public Success(ReservationHolderId reservationHolder) => ReservationHolder = reservationHolder;
                public ReservationHolderId ReservationHolder { get; set; }
            }
        }
    }
}
