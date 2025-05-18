using Domain.Guest;
using Domain.Infrastructure.ReservationHolder;
using Domain.Infrastructure.Rooms;
using Domain.Reservation;
using Domain.Room;

namespace Domain.Infrastructure.Reservations
{
    public class ReservationDataEntity : IReservationDataEntity
    {
        public int Id { get; set; }
        public ReservationId ReservationId { get; set; }

        public RoomId RoomId { get; set; }

        public RoomDataEntity Room { get; set; } = new();

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public ReservationHolderId ReservationHolderId { get; set; }

        public ReservationHolderDataEntity ReservationHolder { get; set; } = new();
    }
}
