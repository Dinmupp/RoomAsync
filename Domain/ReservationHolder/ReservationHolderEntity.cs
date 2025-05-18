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

    public struct ReservationHolderId : IEquatable<ReservationHolderId>
    {
        string _value;

        public ReservationHolderId(string value)
        {
            _value = value;
        }

        public readonly string Value => _value;
        public readonly bool NoValue => string.IsNullOrEmpty(_value);
        public readonly bool HasValue => !NoValue;

        public override readonly int GetHashCode()
            => _value.GetHashCode();

        public readonly bool Equals(ReservationHolderId other) => Equals(other._value);
        public readonly bool Equals(string? other) => string.Equals(_value, other, StringComparison.OrdinalIgnoreCase);

        public override readonly bool Equals(object? obj)
        {
            if (obj is null || GetType() != obj.GetType())
            {
                return false;
            }

            if (obj is ReservationHolderId other)
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

        public static implicit operator ReservationHolderId(string? value)
            => new() { _value = value ?? string.Empty };

        public static implicit operator ReservationHolderId(ReadOnlySpan<char> value)
            => new() { _value = value.ToString() };

        public static bool operator ==(ReservationHolderId left, string? right) => left.Equals(right);
        public static bool operator ==(ReservationHolderId left, ReservationHolderId right) => left.Equals(right);

        public static bool operator !=(ReservationHolderId left, string? right) => !(left == right);
        public static bool operator !=(ReservationHolderId left, ReservationHolderId right) => !(left == right);
    }
}
