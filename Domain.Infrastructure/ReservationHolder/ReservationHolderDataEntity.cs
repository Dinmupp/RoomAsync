using Domain.ReservationHolder;

namespace Domain.Infrastructure.ReservationHolder
{
    public class ReservationHolderDataEntity : IReservationHolderDataEntity
    {
        public int Id { get; set; }
        public ReservationHolderId ReservationHolderId { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CountryCode { get; set; }
        public DateTimeOffset? CheckedInAt { get; set; }
        public DateTimeOffset? CheckedOutAt { get; set; }
    }
}
