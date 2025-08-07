namespace Domain.Room.Response
{
    public class GetAllResponse
    {
        public IReadOnlyList<RoomEntity> Rooms { get; init; } = new List<RoomEntity>();
        public int TotalCount { get; init; } = 0;
    }
}
