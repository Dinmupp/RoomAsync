using Domain.Reservation;
using Domain.Reservation.Driven;
using Domain.Reservation.Request;
using Domain.Reservation.UseCases;
using Domain.ReservationHolder;
using Domain.Room;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.Reservations
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILoggerService _loggerService;

        public ReservationRepository(ApplicationDbContext dbContext, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _loggerService = loggerService;
        }

        public async Task<Result<CreateReservationUseCase.Response.Success, CreateReservationUseCase.Response.Fail>> AddReservationAsync(CreateReservationRequest request, string code, RoomId room, ReservationHolderId reservationHolderId, CancellationToken cancellation = default)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var reservation = new ReservationDataEntity
                    {
                        ReservationId = Guid.NewGuid().ToString(),
                        RoomId = room,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        ReservationHolderId = reservationHolderId,
                        Code = code
                    };
                    var dbReservation = await _dbContext.Reservations.AddAsync(reservation, cancellation);
                    await _dbContext.SaveChangesAsync(cancellation);
                    transaction.Commit();
                    return new CreateReservationUseCase.Response.Success(dbReservation.Entity.ReservationId);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _loggerService.LogError(ex.Message, "Error while adding reservation");
                    throw;
                }
            }
        }

        public async Task<ReservationEntity> GetAsync(ReservationId id, CancellationToken cancellation = default)
        {
            var dbReservation = await _dbContext.Reservations
                 .FirstOrDefaultAsync(r => r.ReservationId == id, cancellation);

            if (dbReservation is null)
            {
                return null!;
            }

            return new ReservationEntity(dbReservation);
        }
    }
}
