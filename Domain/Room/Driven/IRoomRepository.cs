using Domain.Room.UseCase;

namespace Domain.Room.Driven
{
    public interface IRoomRepository
    {
        Task<Result<FindAvailableRoomsUseCase.Response.Success, FindAvailableRoomsUseCase.Response.Fail>> Find(ISpecification<RoomEntity> specification, CancellationToken cancellation = default);
    }
}
