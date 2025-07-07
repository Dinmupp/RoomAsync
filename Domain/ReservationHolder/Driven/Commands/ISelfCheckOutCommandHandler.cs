using Domain.ReservationHolder.Request;
using Domain.ReservationHolder.UseCase;

namespace Domain.ReservationHolder.Driven.Commands
{
    public interface ISelfCheckOutCommandHandler
    {
        Task<Result<SelfCheckOutUseCase.Response.Success, SelfCheckOutUseCase.Response.Fail>> Command(SelfCheckOutRequest request, CancellationToken cancellation = default);
    }
}
