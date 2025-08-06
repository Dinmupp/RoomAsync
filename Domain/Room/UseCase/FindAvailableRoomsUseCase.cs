using Domain.Error;
using Domain.Room.Requests;
using Domain.Room.Specifications;

namespace Domain.Room.UseCase
{
    public sealed class FindAvailableRoomsUseCase
    {
        private readonly RoomSpecificationFactory _roomSpecificationFactory;

        public FindAvailableRoomsUseCase(RoomSpecificationFactory roomSpecificationFactory)
        {
            _roomSpecificationFactory = roomSpecificationFactory;
        }

        public async Task<Result<Response.Success, Response.Fail>> Execute(FindAvailableRoomsRequest request, CancellationToken cancellation = default)
        {
            var specification = _roomSpecificationFactory.CreateFindAvailableRoomsSpecification(request.RoomType);
            var result = await specification.InvokeOnRepository(cancellation);
            if (result is null || !result.Any() || result.Count() == 0)
            {
                return new Response.Fail.NoAvailableRooms();
            }


            return new Response.Success(result);
        }

        public class Response
        {
            public class Fail(string message) : RoomAsyncError(message)
            {
                public class NoAvailableRooms() : Fail("No available rooms to be reserved");
            }

            public class Success
            {
                public Success(IEnumerable<RoomEntity> rooms) => Rooms = rooms;
                public IEnumerable<RoomEntity> Rooms { get; set; }
            }
        }
    }
}
