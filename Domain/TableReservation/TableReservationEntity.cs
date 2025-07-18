using Domain.Reservation;
using Domain.ReservationHolder;
namespace Domain.TableReservation
{
    public class TableReservationEntity : IDataEntityExposer<ITableReservationDataEntity>
    {
        private readonly ITableReservationDataEntity _data;
        public TableReservationEntity(ITableReservationDataEntity data) => _data = data;

        public ReservationId ReservationId => _data.ReservationId;
        public DateTimeOffset StartDate => _data.StartDate;
        public DateTimeOffset EndDate => _data.EndDate;
        public ReservationHolderId ReservationHolderId => _data.ReservationHolderId;
        public int NumberOfGuests => _data.NumberOfGuests;
        public IReadOnlyList<Allergy.Allergy> Allergies =>
            string.IsNullOrWhiteSpace(_data.Allergies)
                ? new List<Allergy.Allergy>()
                : _data.Allergies
                    .Split(',', System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries)
                    .Select(Allergy.Allergy.FromName)
                    .ToList();

        public string? BillToRoomNumber => _data.BillToRoomNumber;
        public bool PaidAtCashier => _data.PaidAtCashier;

        public TDataEntity GetInstanceAs<TDataEntity>() where TDataEntity : class, ITableReservationDataEntity
            => _data as TDataEntity ?? throw new InvalidCastException();
    }
}
