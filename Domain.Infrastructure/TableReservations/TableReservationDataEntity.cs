using Domain.Reservation;
using Domain.ReservationHolder;
using Domain.TableReservation;

namespace Domain.Infrastructure.TableReservations
{
    public class TableReservationDataEntity : BaseDataEntity, ITableReservationDataEntity
    {
        public int Id { get; set; }
        public ReservationId ReservationId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public ReservationHolderId ReservationHolderId { get; set; }
        public int NumberOfGuests { get; set; }
        public string? Allergies { get; set; }
        public string? BillToRoomNumber { get; set; }
        public bool PaidAtCashier { get; set; }
    }
}
