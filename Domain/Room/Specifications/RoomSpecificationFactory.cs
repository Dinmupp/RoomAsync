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

        public GetAllSpecification CreateGetAllSpecification(Range? offset, string roomsNumber, string sortBy, bool sortByAscending)
        {
            return new GetAllSpecification(_roomRepository, offset, roomsNumber, sortBy, sortByAscending);
        }

        public FindAvailableRoomsSpecification CreateFindAvailableRoomsSpecification(RoomType type)
        {
            return new FindAvailableRoomsSpecification(type, _roomRepository);
        }
    }
}
