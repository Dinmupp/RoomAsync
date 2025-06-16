using Domain.Error;
using Domain.Room.Driven;
using Domain.Room.Requests;
using Domain.Room.Specifications;

namespace Domain.Room.UseCase
{
    public sealed class FindAvailableRoomsUseCase
    {
        private readonly IRoomRepository _repository;

        public FindAvailableRoomsUseCase(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Response.Success, Response.Fail>> Execute(FindAvailableRoomsRequest request, CancellationToken cancellation = default)
        {
            var result = await _repository.Find(new FindAvailableRoomsSpecification(request.RoomType), cancellation);

            return result;
        }

        public class Response
        {
            public class Fail(string message) : RoomAsyncError(message)
            {
                public class NoAvailableRooms() : Fail("No available rooms to be reserved");
            }

            public class Success
            {
                public Success(IEnumerable<RoomId> rooms) => Rooms = rooms;
                public IEnumerable<RoomId> Rooms { get; set; }
            }
        }
    }
}
