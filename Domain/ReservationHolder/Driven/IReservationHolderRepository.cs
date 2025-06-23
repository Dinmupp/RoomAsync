using Domain.ReservationHolder.Request;
using Domain.ReservationHolder.UseCase;

namespace Domain.ReservationHolder.Driven
{
    public interface IReservationHolderRepository
    {
        Task<Result<FindReservationHolderUseCase.Response.Success, FindReservationHolderUseCase.Response.Fail>> FindAsync(ISpecification<ReservationHolderEntity> specification, CancellationToken cancellation);

        Task<Result<CreateReservationHolderUseCase.Response.Success, CreateReservationHolderUseCase.Response.Fail>> Create(CreateReservationHolderRequest request, CancellationToken cancellation = default);
    }
}
