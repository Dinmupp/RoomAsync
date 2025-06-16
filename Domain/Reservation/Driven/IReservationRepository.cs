using Domain.Reservation.Request;
using Domain.Reservation.UseCases;
using Domain.Room;

namespace Domain.Reservation.Driven
{
    public interface IReservationRepository
    {
        Task<Result<CreateReservationUseCase.Response.Success, CreateReservationUseCase.Response.Fail>> AddReservationAsync(CreateReservationRequest request, RoomId room, CancellationToken cancellation = default);
    }
}
