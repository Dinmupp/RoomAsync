using Domain.Exceptions;
using Domain.Room;

namespace Domain.Reservation
{
    public class ReservationEntity : IDataEntityExposer<IReservationDataEntity>, IAggregateRoot
    {

        public ReservationId ReservationId => _data.ReservationId ?? string.Empty;
        public RoomId RoomId => _data.RoomId ?? string.Empty;

        public DateTime StartDate => _data.StartDate;
        public DateTime EndDate => _data.EndDate;
        public string GuestName => _data.GuestName ?? string.Empty;
        public string GuestEmail => _data.GuestEmail ?? string.Empty;
        public string GuestPhone => _data.GuestPhone ?? string.Empty;
        TDataEntity IDataEntityExposer<IReservationDataEntity>.GetInstanceAs<TDataEntity>() => (TDataEntity)_data;
    }

    public struct ReservationId : IEquatable<ReservationId>
    {
        string _value;
        public readonly string GetUnsafe() => _value;
        public readonly bool NoValue => string.IsNullOrEmpty(_value);
        public readonly bool HasValue => !NoValue;

        /// <summary>
        /// Returns primitive value. Throws if value is not valid
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidIdException{ReservationId}"></exception>
        public readonly string GetSafe()
            => HasValue
                ? _value
                : throw new InvalidIdException<ReservationId>(_value);

        public readonly bool TryGet(out string value)
        {
            value = _value;
            return HasValue;
        }
        public override readonly int GetHashCode()
            => _value.GetHashCode();

        public readonly bool Equals(ReservationId other) => Equals(other._value);
        public readonly bool Equals(string? other) => string.Equals(_value, other, StringComparison.OrdinalIgnoreCase);

        public override readonly bool Equals(object? obj)
        {
            if (obj is null || GetType() != obj.GetType())
            {
                return false;
            }

            if (obj is RoomId other)
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

        public static implicit operator ReservationId(string? value)
            => new() { _value = value ?? string.Empty };

        public static implicit operator ReservationId(ReadOnlySpan<char> value)
            => new() { _value = value.ToString() };

        public static bool operator ==(ReservationId left, string? right) => left.Equals(right);
        public static bool operator ==(ReservationId left, ReservationId right) => left.Equals(right);

        public static bool operator !=(ReservationId left, string? right) => !(left == right);
        public static bool operator !=(ReservationId left, ReservationId right) => !(left == right);
    }
}
