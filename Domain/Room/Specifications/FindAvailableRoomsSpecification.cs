namespace Domain.Room.Specifications
{
    public class FindAvailableRoomsSpecification : Specification<RoomEntity>
    {
        private readonly RoomType _type;

        public RoomType Type => _type;
        public FindAvailableRoomsSpecification(RoomType type)
        {
            _type = type;
        }
        public override bool IsSatisfiedBy(RoomEntity room)
        {
            return room.Status == RoomStatus.Available;
        }
    }
}
