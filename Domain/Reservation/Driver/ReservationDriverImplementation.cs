using Domain.Reservation.Driven;
using Domain.Reservation.Request;
using Domain.Reservation.UseCases;

namespace Domain.Reservation.Driver
{
    public class ReservationDriverImplementation : IReservationDriverPort
    {
        private readonly CreateReservationUseCase _createReservationUseCase;
        private readonly IReservationRepository _repository;
        public ReservationDriverImplementation(CreateReservationUseCase createReservationUseCase, IReservationRepository repository)
        {
            _createReservationUseCase = createReservationUseCase;
            _repository = repository;
        }

        public async Task<Result<CreateReservationUseCase.Response.Success, CreateReservationUseCase.Response.Fail>> AddReservationAsync(CreateReservationRequest request, CancellationToken cancellation = default) =>
            await _createReservationUseCase.Execute(request, cancellation);

        public async Task<ReservationEntity> GetAsync(ReservationId id, CancellationToken cancellation = default) =>
                await _repository.GetAsync(id, cancellation);
    }
}
