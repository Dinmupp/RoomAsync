namespace Domain.Room.Driven
{
    public interface IRoomRepository
    {
        Task<IEnumerable<RoomEntity>> Find(ISpecification<RoomEntity> specification, CancellationToken cancellation = default);
    }
}
