using Domain.ReservationHolder;

namespace Domain.TableReservation.Request
{
    public class CreateTableReservationRequest
    {
        public required DateTimeOffset StartDate { get; set; }
        public required DateTimeOffset EndDate { get; set; }
        public required ReservationHolderId ReservationHolderId { get; set; }
        public required int NumberOfGuests { get; set; }
        public string? Allergies { get; set; }
        public string? BillToRoomNumber { get; set; }
        public bool PaidAtCashier { get; set; }
    }
}
