using Domain.Exceptions;

namespace Domain.User
{
    public struct UserId : IEquatable<UserId>
    {
        string _value;
        public readonly string GetUnsafe() => _value;
        public readonly bool NoValue => string.IsNullOrEmpty(_value);
        public readonly bool HasValue => !NoValue;

        /// <summary>
        /// Returns primitive value. Throws if value is not valid
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidIdException{UserId}"></exception>
        public readonly string GetSafe()
            => HasValue
                ? _value
                : throw new InvalidIdException<UserId>(_value);

        public readonly bool TryGet(out string value)
        {
            value = _value;
            return HasValue;
        }
        public override readonly int GetHashCode()
            => _value.GetHashCode();

        public readonly bool Equals(UserId other) => Equals(other._value);
        public readonly bool Equals(string? other) => string.Equals(_value, other, StringComparison.OrdinalIgnoreCase);

        public override readonly bool Equals(object? obj)
        {
            if (obj is null || GetType() != obj.GetType())
            {
                return false;
            }

            if (obj is UserId other)
            {
                return Equals(other);
            }

            if (obj is string stringValue)
            {
                return Equals(stringValue);
            }

            return false;
        }

        public override readonly string ToString() => _value;

        public static implicit operator UserId(string? value)
            => new() { _value = value ?? string.Empty };

        public static implicit operator UserId(ReadOnlySpan<char> value)
            => new() { _value = value.ToString() };

        public static bool operator ==(UserId left, string? right) => left.Equals(right);
        public static bool operator ==(UserId left, UserId right) => left.Equals(right);

        public static bool operator !=(UserId left, string? right) => !(left == right);
        public static bool operator !=(UserId left, UserId right) => !(left == right);
    }
}
