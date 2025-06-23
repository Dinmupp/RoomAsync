namespace Domain.ReservationHolder.Specifications
{
    public class GetByNameAndPhoneAndEmail : Specification<ReservationHolderEntity>
    {
        private readonly string _email;
        public string Email => _email;

        private readonly string _phone;
        public string Phone => _phone;

        private readonly string _name;
        public string Name => _name;

        public GetByNameAndPhoneAndEmail(string email, string phone, string name)
        {
            _email = email;
            _phone = phone;
            _name = name;
        }

        public override bool IsSatisfiedBy(ReservationHolderEntity reservationHolder)
        {
            return reservationHolder.Email == _email && reservationHolder.Phone == _phone && reservationHolder.Name == _name;
        }
    }
}
