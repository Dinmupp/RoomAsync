using Domain.ReservationHolder.Request;
using Domain.ReservationHolder.UseCase;

namespace Domain.ReservationHolder.Driver
{
    public class ReservationHolderDriverImplementation : IReservervationHolderDriverPort
    {
        private readonly SelfCheckInUseCase _selfCheckInUseCase;
        private readonly SelfCheckOutUseCase _selfCheckOutUseCase;
        public ReservationHolderDriverImplementation(SelfCheckInUseCase selfCheckInUseCase, SelfCheckOutUseCase selfCheckOutUseCase)
        {
            _selfCheckInUseCase = selfCheckInUseCase;
            _selfCheckOutUseCase = selfCheckOutUseCase;
        }

        public async Task<Result<SelfCheckInUseCase.Response.Success, SelfCheckInUseCase.Response.Fail>> SelfCheckIn(SelfCheckInRequest request, CancellationToken cancellation = default) =>
            await _selfCheckInUseCase.Execute(request, cancellation);

        public async Task<Result<SelfCheckOutUseCase.Response.Success, SelfCheckOutUseCase.Response.Fail>> SelfCheckOut(SelfCheckOutRequest request, CancellationToken cancellation = default) =>
            await _selfCheckOutUseCase.Execute(request, cancellation);
    }
}
