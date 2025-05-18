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
    }
}
