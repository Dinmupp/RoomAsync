namespace Domain.Room
{
    public interface IRoomDataEntity
    {
        public RoomId RoomId { get; }
        public int FloorLevel { get; }
        public int RoomNumber { get; }
        public string Section { get; }
        public RoomStatus Status { get; }
        public string RoomName { get; }
        public string RoomDescription { get; }
        public RoomType RoomType { get; }
    }
}
