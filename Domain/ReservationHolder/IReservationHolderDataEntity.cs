namespace Domain.ReservationHolder
{
    public interface IReservationHolderDataEntity
    {
        public ReservationHolderId ReservationHolderId { get; set; }
        public string Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? CountryCode { get; set; }
        public DateTimeOffset? CheckedInAt { get; set; }
        public DateTimeOffset? CheckedOutAt { get; set; }
    }
}
