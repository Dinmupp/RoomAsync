using Domain.Room;
using Domain.Room.Driven;
using Domain.Room.Specifications;
using Microsoft.EntityFrameworkCore;
using static Domain.Room.Driven.IRoomRepository;

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

        public async Task<RoomResponse> Find(ISpecification<RoomEntity> specification, CancellationToken cancellation = default)
        {
            var result = new List<RoomEntity>();
            var response = new RoomResponse();
            if (specification is FindAvailableRoomsSpecification findAvailableRooms)
            {
                var room = await _dbContext.Rooms
                    .Where(r => r.RoomType == findAvailableRooms.Type && r.Status == Room.RoomStatus.Available).ToArrayAsync(cancellation);

                if (room is null || !room.Any())
                {
                    return response;
                }

                result.AddRange(room.Select(RoomEntity.Create));

                response.Rooms = result;
                response.TotalCount = result.Count;
                return response;
            }



            if (specification is GetAllSpecification getAll)
            {
                var query = _dbContext.Rooms
                    .ConditionalWhere(!string.IsNullOrWhiteSpace(getAll.RoomNumber), x => x.RoomNumber.ToString().StartsWith(getAll.RoomNumber!));

                if (!string.IsNullOrWhiteSpace(getAll.SortBy))
                {
                    var sortExpression = QueryableExtensions.CreateSortExpression<RoomDataEntity>(getAll.SortBy);

                    query = getAll.SortByAscending
                        ? query.OrderBy(sortExpression)
                        : query.OrderByDescending(sortExpression);
                }

                if (getAll.Offset.HasValue && getAll.Offset.Value.End.Value > 0)
                {
                    query = query
                        .Skip(getAll.Offset.Value.Start.Value)
                        .Take(getAll.Offset.Value.End.Value);
                }

                result = await query
                    .Select(r => RoomEntity.Create(r))
                    .ToListAsync(cancellation);

                response.Rooms = result;
                response.TotalCount = await _dbContext.Rooms.CountAsync(cancellation);
                return response;
            }


            throw new NotSupportedException("Unsupported specification for room repository");
        }
    }
}
