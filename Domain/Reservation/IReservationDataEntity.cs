using Domain.Room;

namespace Domain.Reservation
{
    public interface IReservationDataEntity
    {
        public ReservationId ReservationId { get; }
        public RoomId RoomId { get; }

        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public string GuestName { get; }
        public string GuestEmail { get; }
        public string GuestPhone { get; }
    }
}
