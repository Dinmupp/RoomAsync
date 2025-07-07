using Domain.ReservationHolder;
using Domain.Room;

namespace Domain.Reservation
{
    public interface IReservationDataEntity
    {
        public ReservationId ReservationId { get; }
        public RoomId RoomId { get; }

        public DateTimeOffset StartDate { get; }
        public DateTimeOffset EndDate { get; }
        public ReservationHolderId ReservationHolderId { get; }
        public string Code { get; }
        public DateTimeOffset? CheckedOutAt { get; set; }
        public DateTimeOffset? CheckedInAt { get; set; }
    }
}
