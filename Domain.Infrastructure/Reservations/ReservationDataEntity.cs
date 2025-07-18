using Domain.Reservation;
using Domain.ReservationHolder;
using Domain.Room;

namespace Domain.Infrastructure.Reservations
{
    public class ReservationDataEntity : BaseDataEntity, IReservationDataEntity
    {
        public int Id { get; set; }
        public ReservationId ReservationId { get; set; }

        public RoomId RoomId { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public ReservationHolderId ReservationHolderId { get; set; }
        public string Code { get; set; } = string.Empty;
        public int? NumberOfGuests { get; set; }
        public DateTimeOffset? CheckedOutAt { get; set; }
        public DateTimeOffset? CheckedInAt { get; set; }
    }
}
