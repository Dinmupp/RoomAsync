namespace Domain.ReservationHolder.Request
{
    public class CreateReservationHolderRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public ReservationHolderId ReservationHolderId { get; set; }
        public CreateReservationHolderRequest(string name, string email, string phone, ReservationHolderId reservationHolderId)
        {
            Name = name;
            Email = email;
            Phone = phone;
            ReservationHolderId = reservationHolderId;
        }
    }
}
