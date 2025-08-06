using Domain.Room.Driven;

namespace Domain.Room.Specifications
{
    public class RoomSpecificationFactory
    {
        private readonly IRoomRepository _roomRepository;

        public RoomSpecificationFactory(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public GetAllSpecification CreateGetAllSpecification(int? skip, int? take)
        {
            return new GetAllSpecification(_roomRepository, skip, take);
        }

        public FindAvailableRoomsSpecification CreateFindAvailableRoomsSpecification(RoomType type)
        {
            return new FindAvailableRoomsSpecification(type, _roomRepository);
        }
    }
}
