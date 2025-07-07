namespace Domain.ContactWay
{
    public sealed class CountryCode
    {
        public static readonly CountryCode Sweden = new("SE", 46, "Sweden", "0");
        public static readonly CountryCode UnitedStates = new("US", 1, "United States", "1");
        public static readonly CountryCode UnitedKingdom = new("GB", 44, "United Kingdom", "0");
        public static readonly CountryCode Germany = new("DE", 49, "Germany", "0");
        public static readonly CountryCode France = new("FR", 33, "France", "0");
        public static readonly CountryCode India = new("IND", 91, "India", "0");
        public static readonly CountryCode None = new("None", 0, "None", "");

        public string IsoCode { get; }
        public int DialCode { get; }
        public string CountryName { get; }
        public string TrunkPrefix { get; }

        private CountryCode(string isoCode, int dialCode, string countryName, string trunkPrefix)
        {
            IsoCode = isoCode;
            DialCode = dialCode;
            CountryName = countryName;
            TrunkPrefix = trunkPrefix;
        }

        public static IReadOnlyList<CountryCode> All { get; } = new List<CountryCode>
        {
            Sweden, UnitedStates, UnitedKingdom, Germany, France, India
        };

        public static CountryCode FromDialCode(int dialCode) =>
            All.FirstOrDefault(c => c.DialCode == dialCode) ?? None;

        public static CountryCode FromIsoCode(string isoCode) =>
            All.FirstOrDefault(c => c.IsoCode.Equals(isoCode, StringComparison.OrdinalIgnoreCase)) ?? None;

        public override string ToString() => $"+{DialCode} ({CountryName})";
    }

    public readonly struct Phone
    {
        public CountryCode CountryCode { get; }
        public string Number { get; }

        public Phone(CountryCode? countryCode, string? number)
        {
            if (countryCode == null || countryCode == CountryCode.None || !CountryCode.All.Contains(countryCode))
                throw new ArgumentException("Invalid country code.", nameof(countryCode));
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Phone number is required.", nameof(number));
            CountryCode = countryCode;
            Number = number;
        }

        public static CountryCode ParseCountryCode(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return CountryCode.None;

            if (int.TryParse(value, out var intCode))
                return CountryCode.FromDialCode(intCode);

            return CountryCode.FromIsoCode(value);
        }

        public string CountryCodeString => CountryCode.DialCode.ToString();

        public override string ToString() => $"+{CountryCodeString} {Number}";
    }
}
