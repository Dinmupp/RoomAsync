using Domain.Room.Driven;

namespace Domain.Room.Specifications
{
    public class FindAvailableRoomsSpecification : Specification<RoomEntity>
    {
        private readonly IRoomRepository _repository;

        private readonly RoomType _type;

        public RoomType Type => _type;
        public FindAvailableRoomsSpecification(RoomType type, IRoomRepository repository)
        {
            _type = type;
            _repository = repository;
        }
        public override bool IsSatisfiedBy(RoomEntity room)
        {
            return room.Status == RoomStatus.Available;
        }

        public override async Task<IReadOnlyList<RoomEntity>> InvokeOnRepository(CancellationToken cancellation = default)
        {
            var result = await _repository.Find(this, cancellation);

            return result.ToList().AsReadOnly();
        }
    }
}
