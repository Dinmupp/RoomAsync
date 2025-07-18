using Domain.Reservation;
using Domain.TableReservation.Driven;
using Domain.TableReservation.Request;
using Domain.TableReservation.UseCase;

namespace Domain.TableReservation.Driver
{
    public class TableReservationDriverImplementation : ITableReservationDriverPort
    {
        private readonly CreateTableReservationUseCase _useCase;
        private readonly ITableReservationRepository _repository;

        public TableReservationDriverImplementation(CreateTableReservationUseCase useCase, ITableReservationRepository repository)
        {
            _useCase = useCase;
            _repository = repository;
        }

        public async Task<Result<CreateTableReservationUseCase.Response.Success, CreateTableReservationUseCase.Response.Fail>> CreateTableReservationAsync(
            CreateTableReservationRequest request,
            CancellationToken cancellationToken = default)
        {
            return await _useCase.ExecuteAsync(request, cancellationToken);
        }

        public async Task<TableReservationEntity> GetAsync(ReservationId id, CancellationToken cancellation = default) =>
            await _repository.GetAsync(id, cancellation);
    }
}
