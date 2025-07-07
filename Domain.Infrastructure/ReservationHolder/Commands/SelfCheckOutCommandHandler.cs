using Domain.ReservationHolder.Driven.Commands;
using Domain.ReservationHolder.Request;
using Domain.ReservationHolder.UseCase;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.ReservationHolder.Commands
{
    public class SelfCheckOutCommandHandler : ISelfCheckOutCommandHandler
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILoggerService _loggerService;

        public SelfCheckOutCommandHandler(ApplicationDbContext dbContext, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _loggerService = loggerService;
        }


        public async Task<Result<SelfCheckOutUseCase.Response.Success, SelfCheckOutUseCase.Response.Fail>> Command(SelfCheckOutRequest request, CancellationToken cancellation = default)
        {
            try
            {
                var reservation = await _dbContext.Reservations.FirstOrDefaultAsync(r => r.Code == request.Code && r.CheckedOutAt == null, cancellation);

                var holder = await _dbContext.ReservationHolders
                    .FirstOrDefaultAsync(rh => rh.ReservationHolderId == reservation!.ReservationHolderId, cancellation);

                if (holder == null)
                    return new SelfCheckOutUseCase.Response.Fail.NotFound();

                holder.CheckedOutAt = DateTimeOffset.UtcNow;
                reservation!.CheckedOutAt = DateTimeOffset.UtcNow;
                await _dbContext.SaveChangesAsync(cancellation);

                return new SelfCheckOutUseCase.Response.Success(holder.ReservationHolderId);
            }
            catch (Exception e)
            {
                return new SelfCheckOutUseCase.Response.Fail.Error(e.Message);
            }
        }
    }
}
