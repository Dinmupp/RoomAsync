using Domain.ReservationHolder.Request;
using Domain.ReservationHolder.UseCase;

namespace Domain.ReservationHolder.Driver
{
    public interface IReservervationHolderDriverPort
    {
        Task<Result<SelfCheckInUseCase.Response.Success, SelfCheckInUseCase.Response.Fail>> SelfCheckIn(SelfCheckInRequest request, CancellationToken cancellation = default);

        Task<Result<SelfCheckOutUseCase.Response.Success, SelfCheckOutUseCase.Response.Fail>> SelfCheckOut(SelfCheckOutRequest request, CancellationToken cancellation = default);
    }
}
