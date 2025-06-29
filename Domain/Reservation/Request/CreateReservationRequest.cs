using Domain.ContactWay;
using Domain.ReservationHolder;
using Domain.Room;

namespace Domain.Reservation.Request
{
    public class CreateReservationRequest
    {
        public required RoomType RoomType { get; set; }
        public required DateTimeOffset StartDate { get; set; }
        public required DateTimeOffset EndDate { get; set; }
        public required string ReservationHolderName { get; set; }
        public required Email ReservationHolderEmail { get; set; }
        public required Phone ReservationHolderPhone { get; set; }
        public required ReservationHolderId ReservationHolderId { get; set; }
    }
}
