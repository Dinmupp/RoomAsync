using Domain.Room;
using Domain.Room.Driven;
using Domain.Room.Specifications;
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

        public async Task<IEnumerable<RoomEntity>> Find(ISpecification<RoomEntity> specification, CancellationToken cancellation = default)
        {
            var result = new List<RoomEntity>();
            if (specification is FindAvailableRoomsSpecification findAvailableRooms)
            {
                var room = await _dbContext.Rooms
                    .Where(r => r.RoomType == findAvailableRooms.Type && r.Status == Room.RoomStatus.Available).ToArrayAsync(cancellation);

                if (room is null || !room.Any())
                {
                    return new List<RoomEntity>();
                }

                result.AddRange(room.Select(RoomEntity.Create));

                return result;
            }

            if (specification is GetAllSpecification getAll)
            {
                if (getAll.Skip.HasValue && getAll.Take.HasValue)
                {
                    return await _dbContext.Rooms
                    .OrderBy(r => r.RoomId)
                    .Skip(getAll.Skip.Value)
                    .Take(getAll.Take.Value)
                    .Select(r => RoomEntity.Create(r))
                    .ToListAsync(cancellation);
                }

                return await _dbContext.Rooms
                                   .OrderBy(r => r.RoomId)
                                   .Select(r => RoomEntity.Create(r))
                                   .ToListAsync(cancellation);
            }

            throw new NotSupportedException("Unsupported specification for room repository");
        }
    }
}
