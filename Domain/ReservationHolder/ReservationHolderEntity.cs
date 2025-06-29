using Domain.ContactWay;

namespace Domain.ReservationHolder
{
    public class ReservationHolderEntity : IDataEntityExposer<IReservationHolderDataEntity>, IAggregateRoot
    {
        public ReservationHolderId ReservationHolderId => _data.ReservationHolderId;
        public string Name => _data.Name;

        public Email Email => new Email(_data.Email);

        public Phone Phone => new Phone(Phone.ParseCountryCode(_data.CountryCode), _data.Phone);

        private readonly IReservationHolderDataEntity _data;
        private ReservationHolderEntity(IReservationHolderDataEntity data)
        {
            _data = data;
        }

        public static ReservationHolderEntity Create(IReservationHolderDataEntity data)
        {
            return new ReservationHolderEntity(data);
        }
        TDataEntity IDataEntityExposer<IReservationHolderDataEntity>.GetInstanceAs<TDataEntity>() => (TDataEntity)_data;
    }
}
