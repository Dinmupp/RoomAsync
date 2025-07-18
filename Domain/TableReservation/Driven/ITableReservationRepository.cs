using Domain.Reservation;
using Domain.TableReservation.Request;
using Domain.TableReservation.UseCase;

namespace Domain.TableReservation.Driven
{
    public interface ITableReservationRepository
    {
        Task<TableReservationEntity> GetAsync(ReservationId id, CancellationToken cancellation = default);
        Task<Result<CreateTableReservationUseCase.Response.Success, CreateTableReservationUseCase.Response.Fail>> AddTableReservationAsync(
            CreateTableReservationRequest request,
            CancellationToken cancellation = default);
    }
}
