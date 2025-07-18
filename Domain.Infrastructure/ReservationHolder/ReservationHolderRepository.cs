using Domain.ReservationHolder;
using Domain.ReservationHolder.Driven;
using Domain.ReservationHolder.Request;
using Domain.ReservationHolder.Specifications;
using Domain.ReservationHolder.UseCase;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.ReservationHolder
{
    public class ReservationHolderRepository : IReservationHolderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILoggerService _loggerService;

        public ReservationHolderRepository(ApplicationDbContext dbContext, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _loggerService = loggerService;
        }

        public async Task<Result<CreateReservationHolderUseCase.Response.Success, CreateReservationHolderUseCase.Response.Fail>> Create(CreateReservationHolderRequest request, CancellationToken cancellation = default)
        {
            try
            {
                var result = await _dbContext.ReservationHolders.AddAsync(new ReservationHolderDataEntity
                {
                    ReservationHolderId = request.ReservationHolderId,
                    Name = request.Name,
                    Phone = request.Phone.Number,
                    CountryCode = request.Phone.CountryCodeString,
                    Email = request.Email.Value,
                    CreatedDate = DateTimeOffset.UtcNow
                }, cancellation);

                return new CreateReservationHolderUseCase.Response.Success(result.Entity.ReservationHolderId);
            }
            catch (Exception e)
            {
                return new CreateReservationHolderUseCase.Response.Fail.CreateReservationHolderError(e.Message);
            }
        }

        public async Task<Result<FindReservationHolderUseCase.Response.Success, FindReservationHolderUseCase.Response.Fail>> FindAsync(ISpecification<ReservationHolderEntity> specification, CancellationToken cancellation)
        {
            if (specification is GetByNameAndPhoneAndEmail getByNameAndPhoneAndEmail)
            {
                var reservationHolderExists = await _dbContext.ReservationHolders
                       .AnyAsync(rh => rh.Name == getByNameAndPhoneAndEmail.Name &&
                        (rh.Phone == getByNameAndPhoneAndEmail.Phone.Number && rh.CountryCode == getByNameAndPhoneAndEmail.Phone.CountryCodeString) &&
                        rh.Email == getByNameAndPhoneAndEmail.Email.Value, cancellation);

                if (!reservationHolderExists)
                {
                    return new FindReservationHolderUseCase.Response.Fail.DidNotFindReservationHolder();
                }

                var result = await _dbContext.ReservationHolders
                   .Where(rh => rh.Name == getByNameAndPhoneAndEmail.Name &&
                              (rh.Phone == getByNameAndPhoneAndEmail.Phone.Number && rh.CountryCode == getByNameAndPhoneAndEmail.Phone.CountryCodeString) &&
                              rh.Email == getByNameAndPhoneAndEmail.Email.Value).ToListAsync(cancellation);

                return new FindReservationHolderUseCase.Response.Success(result.Select(rh => rh.ReservationHolderId));
            }

            return new FindReservationHolderUseCase.Response.Fail.UndefinedSpecification();
        }
    }
}
