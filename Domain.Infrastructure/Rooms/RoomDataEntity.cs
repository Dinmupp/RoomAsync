using Domain.Room;

namespace Domain.Infrastructure.Rooms
{
    public class RoomDataEntity : BaseDataEntity, IRoomDataEntity
    {
        public int Id { get; set; }
        public RoomId RoomId { get; set; }

        public int FloorLevel { get; set; }

        public int RoomNumber { get; set; }

        public string Section { get; set; } = string.Empty;

        public RoomStatus Status { get; set; }

        public string RoomName { get; set; } = string.Empty;

        public string RoomDescription { get; set; } = string.Empty;

        public RoomType RoomType { get; set; }
    }
}
