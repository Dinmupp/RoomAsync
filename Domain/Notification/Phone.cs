namespace Domain.ContactWay
{
    public readonly struct Phone
    {
        public string CountryCode { get; }
        public string Number { get; }

        public Phone(string? countryCode, string? number)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                throw new ArgumentException("Country code is required.", nameof(countryCode));
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Phone number is required.", nameof(number));
            CountryCode = countryCode;
            Number = number;
        }

        public override string ToString() => $"+{CountryCode}{Number}";
    }
}
