using Domain.ReservationHolder;
using Domain.Room;

namespace Domain.Reservation
{
    public class ReservationEntity : IDataEntityExposer<IReservationDataEntity>, IAggregateRoot
    {
        private readonly IReservationDataEntity _data;
        public ReservationEntity(IReservationDataEntity data)
        {
            _data = data;
        }

        public ReservationId ReservationId => _data.ReservationId;
        public RoomId RoomId => _data.RoomId;

        public DateTimeOffset StartDate => _data.StartDate;
        public DateTimeOffset EndDate => _data.EndDate;
        public ReservationHolderId ReservationHolderId => _data.ReservationHolderId;

        TDataEntity IDataEntityExposer<IReservationDataEntity>.GetInstanceAs<TDataEntity>() => (TDataEntity)_data;
    }

    public struct ReservationId : IEquatable<ReservationId>
    {
        public readonly string Value => _value;
        private string _value;

        public ReservationId(string value)
        {
            _value = value;
        }

        public readonly bool NoValue => string.IsNullOrEmpty(_value);
        public readonly bool HasValue => !NoValue;

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
