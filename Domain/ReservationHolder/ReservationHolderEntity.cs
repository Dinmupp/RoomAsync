namespace Domain.ReservationHolder
{
    public class ReservationHolderEntity : IDataEntityExposer<IReservationHolderDataEntity>, IAggregateRoot
    {
        public ReservationHolderId ReservationHolderId => _data.ReservationHolderId;
        public string Name => _data.Name;

        public string Email => _data.Email;

        public string Phone => _data.Phone;

        private readonly IReservationHolderDataEntity _data;
        public ReservationHolderEntity(IReservationHolderDataEntity data)
        {
            _data = data;
        }
        TDataEntity IDataEntityExposer<IReservationHolderDataEntity>.GetInstanceAs<TDataEntity>() => (TDataEntity)_data;
    }
}
