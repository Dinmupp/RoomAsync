using Domain.Room.Driven;

namespace Domain.Room.Specifications
{
    public class GetAllSpecification : Specification<RoomEntity>
    {
        private readonly IRoomRepository _repo;

        private readonly int? _skip;
        private readonly int? _take;

        public int? Skip => _skip;
        public int? Take => _take;
        public GetAllSpecification(IRoomRepository repo, int? skip, int? take)
        {
            _skip = skip;
            _take = take;
            _repo = repo;
        }
        public override bool IsSatisfiedBy(RoomEntity room)
        {
            return true; // This specification does not filter by any specific criteria, so it always returns true.
        }

        public override async Task<IReadOnlyList<RoomEntity>> InvokeOnRepository(CancellationToken cancellation = default)
        {
            var result = await _repo.Find(this, cancellation);

            return result.ToList().AsReadOnly();
        }

    }
}
