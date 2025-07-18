using Domain.Reservation;
using Domain.TableReservation;
using Domain.TableReservation.Driven;
using Domain.TableReservation.Request;
using Domain.TableReservation.UseCase;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.TableReservations
{
    public class TableReservationRepository : ITableReservationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILoggerService _loggerService;

        public TableReservationRepository(ApplicationDbContext dbContext, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _loggerService = loggerService;
        }

        public async Task<Result<CreateTableReservationUseCase.Response.Success, CreateTableReservationUseCase.Response.Fail>> AddTableReservationAsync(
            CreateTableReservationRequest request,
            CancellationToken cancellation = default)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var reservation = new TableReservationDataEntity
                {
                    ReservationId = new ReservationId(Guid.NewGuid().ToString()),
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    ReservationHolderId = request.ReservationHolderId,
                    NumberOfGuests = request.NumberOfGuests,
                    Allergies = request.Allergies,
                    BillToRoomNumber = request.BillToRoomNumber,
                    PaidAtCashier = request.PaidAtCashier,
                    CreatedDate = DateTimeOffset.UtcNow

                };
                var dbReservation = await _dbContext.Set<TableReservationDataEntity>().AddAsync(reservation, cancellation);
                await _dbContext.SaveChangesAsync(cancellation);
                transaction.Commit();
                return new CreateTableReservationUseCase.Response.Success(dbReservation.Entity.ReservationId);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _loggerService.LogError(ex.Message, "Error while adding table reservation");
                throw;
            }
        }

        public async Task<TableReservationEntity> GetAsync(ReservationId id, CancellationToken cancellation = default)
        {
            var dbReservation = await _dbContext.Set<TableReservationDataEntity>()
                .FirstOrDefaultAsync(r => r.ReservationId == id, cancellation);

            if (dbReservation is null)
                throw new InvalidOperationException("Reservation not found");

            return new TableReservationEntity(dbReservation);
        }
    }
}
