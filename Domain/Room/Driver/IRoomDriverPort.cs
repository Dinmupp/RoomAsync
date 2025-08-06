namespace Domain.Room.Driver
{
    public interface IRoomDriverPort
    {
        Task<IReadOnlyList<RoomEntity>> FindAsync(ISpecification<RoomEntity> specification, CancellationToken cancellation = default);
    }
}
