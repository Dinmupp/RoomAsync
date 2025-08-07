using Domain.Room.Response;

namespace Domain.Room.Driver
{
    public interface IRoomDriverPort
    {
        Task<GetAllResponse> FindAsync(ISpecification<RoomEntity> specification, CancellationToken cancellation = default);
    }
}
