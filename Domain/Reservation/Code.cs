using System.Text.RegularExpressions;

namespace Domain.Reservation
{
    public struct Code : IEquatable<Code>
    {
        private static readonly Regex CodePattern = new("^[A-Z0-9]{6}$", RegexOptions.Compiled);
        public readonly string Value => _value;
        private string _value;

        public Code(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Code cannot be null or empty.", nameof(value));
            value = value.ToUpperInvariant();
            if (!CodePattern.IsMatch(value))
                throw new ArgumentException("Code must be 6 characters, A-Z or 0-9.", nameof(value));
            _value = value;
        }

        public override string ToString() => _value;

        public bool Equals(Code other) => _value == other._value;
        public override bool Equals(object? obj) => obj is Code other && Equals(other);
        public override int GetHashCode() => _value.GetHashCode();


        public static implicit operator Code(string? value)
            => new() { _value = value ?? string.Empty };

        public static implicit operator Code(ReadOnlySpan<char> value)
            => new() { _value = value.ToString() };

        public static bool operator ==(Code left, string? right) => left.Equals(right);
        public static bool operator ==(Code left, Code right) => left.Equals(right);

        public static bool operator !=(Code left, string? right) => !(left == right);
        public static bool operator !=(Code left, Code right) => !(left == right);
    }
}
