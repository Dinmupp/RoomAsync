using Domain.Reservation.Request;
using Domain.Reservation.UseCases;

namespace Domain.Reservation.Driver
{
    public interface IReservationDriverPort
    {
        Task<Result<CreateReservationUseCase.Response.Success, CreateReservationUseCase.Response.Fail>> AddReservationAsync(CreateReservationRequest request, CancellationToken cancellation = default);
    }
}
