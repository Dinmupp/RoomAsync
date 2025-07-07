using Domain.ReservationHolder.Driven.Commands;
using Domain.ReservationHolder.Request;
using Domain.ReservationHolder.UseCase;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.ReservationHolder.Commands
{
    public class SelfCheckInCommandHandler : ISelfCheckInCommandHandler
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILoggerService _loggerService;

        public SelfCheckInCommandHandler(ApplicationDbContext dbContext, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _loggerService = loggerService;
        }


        public async Task<Result<SelfCheckInUseCase.Response.Success, SelfCheckInUseCase.Response.Fail>> Command(SelfCheckInRequest request, CancellationToken cancellation = default)
        {
            try
            {
                var reservation = await _dbContext.Reservations.FirstOrDefaultAsync(r => r.Code == request.Code && r.CheckedOutAt == null && r.StartDate.Day == DateTime.Today.Day, cancellation);

                var holder = await _dbContext.ReservationHolders
                    .FirstOrDefaultAsync(rh => rh.ReservationHolderId == reservation!.ReservationHolderId, cancellation);

                if (holder == null)
                    return new SelfCheckInUseCase.Response.Fail.NotFound();

                holder.CheckedInAt = DateTimeOffset.UtcNow;
                reservation!.CheckedInAt = DateTimeOffset.UtcNow;
                await _dbContext.SaveChangesAsync(cancellation);

                return new SelfCheckInUseCase.Response.Success(holder.ReservationHolderId);
            }
            catch (Exception e)
            {
                return new SelfCheckInUseCase.Response.Fail.Error(e.Message);
            }
        }
    }
}
