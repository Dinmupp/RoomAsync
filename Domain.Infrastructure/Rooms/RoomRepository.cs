using Domain.Room;
using Domain.Room.Driven;
using Domain.Room.Specifications;
using Domain.Room.UseCase;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.Rooms
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILoggerService _loggerService;

        public RoomRepository(ApplicationDbContext dbContext, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _loggerService = loggerService;
        }

        public async Task<Result<FindAvailableRoomsUseCase.Response.Success, FindAvailableRoomsUseCase.Response.Fail>> Find(ISpecification<RoomEntity> specification, CancellationToken cancellation = default)
        {
            var result = new List<RoomEntity>();
            if (specification is FindAvailableRoomsSpecification findAvailableRooms)
            {
                var room = await _dbContext.Rooms
                    .FirstOrDefaultAsync(r => r.RoomType == findAvailableRooms.Type && r.Status == Room.RoomStatus.Available, cancellation);

                if (room is null)
                {
                    return new FindAvailableRoomsUseCase.Response.Fail.NoAvailableRooms();
                }

                result.Add(RoomEntity.Create(room));

                return new FindAvailableRoomsUseCase.Response.Success(result);
            }
            throw new NotSupportedException("Unsupported specification for room repository");
        }
    }
}
