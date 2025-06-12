using Domain.Reservation.Driven;
using Domain.Reservation.Request;
using Domain.Reservation.UseCases;
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

        public async Task<Result<CreateReservationUseCase.Response.Success, CreateReservationUseCase.Response.Fail>> AddReservationAsync(CreateReservationRequest request, CancellationToken cancellation = default)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var dbReservationHolder = await FindReservationHolder(request, cancellation);

                    var room = await _dbContext.Rooms
                        .FirstOrDefaultAsync(r => r.RoomType == request.RoomType && r.Status == Room.RoomStatus.Available, cancellation);

                    if (room is null)
                    {
                        return new CreateReservationUseCase.Response.Fail.RoomAlreadyReserved();
                    }

                    var reservation = new ReservationDataEntity
                    {
                        ReservationId = Guid.NewGuid().ToString(),
                        RoomId = room.RoomId,
                        Room = room,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        ReservationHolderId = dbReservationHolder.ReservationHolderId,
                        ReservationHolder = dbReservationHolder
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

        private async Task<ReservationHolder.ReservationHolderDataEntity> FindReservationHolder(CreateReservationRequest request, CancellationToken cancellation)
        {
            var reservationHolderExists = await _dbContext.ReservationHolders
                    .AnyAsync(rh => rh.Name == request.ReservationHolderName &&
                     rh.Phone == request.ReservationHolderPhone &&
                     rh.Email == request.ReservationHolderEmail, cancellation);

            if (!reservationHolderExists)
            {
                var result = await _dbContext.ReservationHolders.AddAsync(new ReservationHolder.ReservationHolderDataEntity
                {
                    ReservationHolderId = request.ReservationHolderId,
                    Name = request.ReservationHolderName,
                    Phone = request.ReservationHolderPhone,
                    Email = request.ReservationHolderEmail
                }, cancellation);

                return result.Entity;
            }

            return await _dbContext.ReservationHolders
                    .FirstAsync(rh => rh.Name == request.ReservationHolderName &&
                               rh.Phone == request.ReservationHolderPhone &&
                               rh.Email == request.ReservationHolderEmail, cancellation);
        }
    }
}
