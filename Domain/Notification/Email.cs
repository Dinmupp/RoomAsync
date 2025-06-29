using System.Text.RegularExpressions;

namespace Domain.ContactWay
{
    public readonly struct Email
    {
        public string? Value { get; }
        public Email(string? value)
        {
            if (!IsValid(value))
                throw new ArgumentException("Invalid email format.", nameof(value));
            Value = value;
        }

        public static bool IsValid(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            // RFC 5322 Official Standard regex (simplified for practical use)
            const string pattern =
                @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)@))" +
                @"((\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z]{2,}))$";
            return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
        }

        public static string[] CommonDomains => new[] { "gmail.com", "outlook.com", "yahoo.com" };

        public override string? ToString() => Value;
    }
}
