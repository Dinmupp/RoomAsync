using Domain.Room.Driven;

namespace Domain.Room.Specifications
{
    public class GetAllSpecification : Specification<RoomEntity>
    {
        private readonly IRoomRepository _repo;

        private readonly Range? _offset;


        public Range? Offset => _offset;

        private readonly string? _roomNumber;
        public string? RoomNumber => _roomNumber;

        private string? _sortBy;
        public string? SortBy => _sortBy;

        private bool _sortByAscending;
        public bool SortByAscending => _sortByAscending;

        public GetAllSpecification(IRoomRepository repo, Range? offset, string? roomNumber, string? sortBy, bool sortByAscending)
        {
            _offset = offset;
            _repo = repo;
            _roomNumber = roomNumber;
            _sortBy = sortBy;
            _sortByAscending = sortByAscending;
        }
        public override bool IsSatisfiedBy(RoomEntity room)
        {
            return true; // This specification does not filter by any specific criteria, so it always returns true.
        }

        public override async Task<(IReadOnlyList<RoomEntity> items, int totalCount)> InvokeOnRepository(CancellationToken cancellation = default)
        {
            var result = await _repo.Find(this, cancellation);

            return (items: result.Rooms, totalCount: result.TotalCount);
        }
    }
}
