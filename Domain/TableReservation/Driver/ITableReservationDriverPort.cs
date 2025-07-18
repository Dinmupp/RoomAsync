using Domain.Reservation;
using Domain.TableReservation.Request;
using Domain.TableReservation.UseCase;

namespace Domain.TableReservation.Driver
{
    public interface ITableReservationDriverPort
    {
        Task<Result<CreateTableReservationUseCase.Response.Success, CreateTableReservationUseCase.Response.Fail>> CreateTableReservationAsync(
        CreateTableReservationRequest request,
        CancellationToken cancellationToken = default);

        Task<TableReservationEntity> GetAsync(ReservationId id, CancellationToken cancellation = default);
    }
}
