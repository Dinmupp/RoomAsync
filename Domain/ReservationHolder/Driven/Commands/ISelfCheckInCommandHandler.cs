using Domain.ReservationHolder.Request;
using Domain.ReservationHolder.UseCase;

namespace Domain.ReservationHolder.Driven.Commands
{
    public interface ISelfCheckInCommandHandler
    {
        Task<Result<SelfCheckInUseCase.Response.Success, SelfCheckInUseCase.Response.Fail>> Command(SelfCheckInRequest request, CancellationToken cancellation = default);
    }
}
