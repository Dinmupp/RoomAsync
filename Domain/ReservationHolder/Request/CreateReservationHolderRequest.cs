using Domain.ContactWay;

namespace Domain.ReservationHolder.Request
{
    public class CreateReservationHolderRequest
    {
        public string Name { get; set; } = string.Empty;
        public Email Email { get; set; }
        public Phone Phone { get; set; }
        public ReservationHolderId ReservationHolderId { get; set; }
        public CreateReservationHolderRequest(string name, Email email, Phone phone, ReservationHolderId reservationHolderId)
        {
            Name = name;
            Email = email;
            Phone = phone;
            ReservationHolderId = reservationHolderId;
        }
    }
}
