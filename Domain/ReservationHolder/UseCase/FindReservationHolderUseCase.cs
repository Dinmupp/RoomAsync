using Domain.Error;
using Domain.ReservationHolder.Driven;

namespace Domain.ReservationHolder.UseCase
{
    public sealed class FindReservationHolderUseCase
    {
        private readonly IReservationHolderRepository _repository;

        public FindReservationHolderUseCase(IReservationHolderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Response.Success, Response.Fail>> Execute(ISpecification<ReservationHolderEntity> specification, CancellationToken cancellation = default)
        {
            var result = await _repository.FindAsync(specification, cancellation);

            return result;
        }

        public class Response
        {
            public class Fail(string message) : RoomAsyncError(message)
            {
                public class DidNotFindReservationHolder() : Fail("Could not find any ReservationHolder");

                public class UndefinedSpecification() : Fail("The specification is undefined or not supported.");
            }

            public class Success
            {
                public Success(IEnumerable<ReservationHolderId> reservationHolders) => ReservationHolders = reservationHolders;
                public IEnumerable<ReservationHolderId> ReservationHolders { get; set; }
            }
        }
    }
}
