namespace Domain.ContactWay
{
    public enum CountryCode
    {
        SE = 46,   // Sweden
        US = 1,    // United States
        GB = 44,   // United Kingdom
        DE = 49,   // Germany
        FR = 33,   // France
        IND = 91,   // India
        None = 0,   // No country code
        // Add more as needed
    }
    public readonly struct Phone
    {
        public CountryCode CountryCode { get; }
        public string Number { get; }

        public Phone(CountryCode? countryCode, string? number)
        {
            if (countryCode == null || !Enum.IsDefined(typeof(CountryCode), countryCode) || countryCode == CountryCode.None)
                throw new ArgumentException("Invalid country code.", nameof(countryCode));
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Phone number is required.", nameof(number));
            CountryCode = countryCode.Value;
            Number = number;
        }

        public static CountryCode ParseCountryCode(string? value)
        {
            CountryCode code;
            if (string.IsNullOrWhiteSpace(value))
                return CountryCode.None;

            // Try parse as integer (e.g., "46")
            if (int.TryParse(value, out var intCode))
            {
                if (Enum.IsDefined(typeof(CountryCode), intCode))
                {
                    code = (CountryCode)intCode;
                    return code;
                }
            }

            // Try parse as enum name (e.g., "SE")
            if (Enum.TryParse<CountryCode>(value, true, out var namedCode))
            {
                code = namedCode;
                return code;
            }

            return CountryCode.None;
        }


        public string CountryCodeString => ((int)CountryCode).ToString();

        public override string ToString() => $"+{CountryCodeString} {Number}";
    }
}
