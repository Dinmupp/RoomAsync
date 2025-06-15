namespace Domain.Room
{
    public enum RoomType
    {
        Single = 0,
        Double = 1,
        Suite = 2,
        Deluxe = 3,
        Family = 4,
        Executive = 5,
        Conference = 6,
        Standard = 7,
        Meeting = 8
    }
    public enum RoomStatus
    {
        Available = 0,
        Occupied = 1,
        Maintenance = 2,
        OutOfService = 3,
        Cleaning = 4,
        Reserved = 5
    }
    public class RoomEntity : IDataEntityExposer<IRoomDataEntity>, IAggregateRoot
    {
        public RoomId RoomId => _data.RoomId;
        public int FloorLevel => _data.FloorLevel;
        public int RoomNumber => _data.RoomNumber;
        public string Section => _data.Section;
        public RoomStatus Status => _data.Status;
        public string RoomName => _data.RoomName;
        public string RoomDescription => _data.RoomDescription;
        public RoomType RoomType => _data.RoomType;
        private readonly IRoomDataEntity _data;
        private RoomEntity(IRoomDataEntity data)
        {
            _data = data;
        }

        public static RoomEntity Create(IRoomDataEntity data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Room data entity cannot be null.");
            }
            return new RoomEntity(data);
        }
        TDataEntity IDataEntityExposer<IRoomDataEntity>.GetInstanceAs<TDataEntity>() => (TDataEntity)_data;
    }

    public struct RoomId : IEquatable<RoomId>
    {
        string _value;

        public RoomId(string value)
        {
            _value = value;
        }

        public readonly string Value => _value;
        public readonly bool NoValue => string.IsNullOrEmpty(_value);
        public readonly bool HasValue => !NoValue;

        public override readonly int GetHashCode()
            => _value.GetHashCode();

        public readonly bool Equals(RoomId other) => Equals(other._value);
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

        public static implicit operator RoomId(string? value)
            => new() { _value = value ?? string.Empty };

        public static implicit operator RoomId(ReadOnlySpan<char> value)
            => new() { _value = value.ToString() };

        public static bool operator ==(RoomId left, string? right) => left.Equals(right);
        public static bool operator ==(RoomId left, RoomId right) => left.Equals(right);

        public static bool operator !=(RoomId left, string? right) => !(left == right);
        public static bool operator !=(RoomId left, RoomId right) => !(left == right);
    }
}
