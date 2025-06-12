using Domain.Reservation.Request;
using Domain.Reservation.UseCases;

namespace Domain.Reservation.Driver
{
    public class ReservationDriverImplementation : IReservationDriverPort
    {
        private readonly CreateReservationUseCase _createReservationUseCase;
        public ReservationDriverImplementation(CreateReservationUseCase createReservationUseCase)
        {
            _createReservationUseCase = createReservationUseCase;
        }

        public async Task<Result<CreateReservationUseCase.Response.Success, CreateReservationUseCase.Response.Fail>> AddReservationAsync(CreateReservationRequest request, CancellationToken cancellation = default) =>
            await _createReservationUseCase.Execute(request, cancellation);
    }
}
