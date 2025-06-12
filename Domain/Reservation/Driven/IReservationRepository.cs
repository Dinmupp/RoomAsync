using Domain.Reservation.Request;
using Domain.Reservation.UseCases;

namespace Domain.Reservation.Driven
{
    public interface IReservationRepository
    {
        Task<Result<CreateReservationUseCase.Response.Success, CreateReservationUseCase.Response.Fail>> AddReservationAsync(CreateReservationRequest request, CancellationToken cancellation = default);
    }
}
