using Domain.ContactWay;

namespace Domain.ReservationHolder.Specifications
{
    public class GetByNameAndPhoneAndEmail : Specification<ReservationHolderEntity>
    {
        private readonly Email _email;
        public Email Email => _email;

        private readonly Phone _phone;
        public Phone Phone => _phone;

        private readonly string _name;
        public string Name => _name;

        public GetByNameAndPhoneAndEmail(Email email, Phone phone, string name)
        {
            _email = email;
            _phone = phone;
            _name = name;
        }

        public override bool IsSatisfiedBy(ReservationHolderEntity reservationHolder)
        {
            return reservationHolder.Email.Value == _email.Value && reservationHolder.ToString() == _phone.ToString() && reservationHolder.Name == _name;
        }
    }
}
