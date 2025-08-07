namespace Domain.Room.Driven
{
    public interface IRoomRepository
    {
        Task<RoomResponse> Find(ISpecification<RoomEntity> specification, CancellationToken cancellation = default);

        public class RoomResponse
        {
            public IReadOnlyList<RoomEntity> Rooms { get; set; } = new List<RoomEntity>();
            public int TotalCount { get; set; } = 0;
        }
    }
}
