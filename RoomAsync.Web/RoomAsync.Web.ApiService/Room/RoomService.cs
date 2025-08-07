using Domain.Room.Driver;
using Domain.Room.Response;
using Domain.Room.Specifications;

namespace RoomAsync.Web.ApiService.Room
{
    public class RoomService
    {

        private readonly IRoomDriverPort _roomDriverPort;
        private readonly RoomSpecificationFactory _roomSpecificationFactory;
        public RoomService(IRoomDriverPort roomDriverPort, RoomSpecificationFactory roomSpecificationFactory)
        {
            _roomDriverPort = roomDriverPort;
            _roomSpecificationFactory = roomSpecificationFactory;
        }

        public async Task<GetAllResponse> GetAllAsync(GetAllRoomsRequest request, CancellationToken cancellation)
        {
            var spec = _roomSpecificationFactory.CreateGetAllSpecification(new Range(request.StartIndex ?? 0, request.Count ?? 0), request.RoomNumber ?? string.Empty, request.SortBy ?? string.Empty, request.SortByAscending);

            return await _roomDriverPort.FindAsync(spec, cancellation);


        }
    }
}
