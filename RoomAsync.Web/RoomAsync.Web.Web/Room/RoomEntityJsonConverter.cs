using Domain;
using Domain.Room;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RoomAsync.Web.Web.Room
{
    public class RoomEntityJsonConverter : JsonConverter<RoomEntity>
    {
        public override RoomEntity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dto = JsonSerializer.Deserialize<RoomDataDto>(ref reader, options);
            if (dto == null)
            {
                throw new JsonException("Failed to deserialize RoomEntity.");
            }

            return RoomEntity.Create(dto);
        }

        public override void Write(Utf8JsonWriter writer, RoomEntity value, JsonSerializerOptions options)
        {
            var data = ((IDataEntityExposer<IRoomDataEntity>)value).GetInstanceAs<RoomDataDto>();
            JsonSerializer.Serialize(writer, data, options);
        }
    }


    public class RoomDataDto : IRoomDataEntity
    {
        public RoomId RoomId { get; set; }
        public int FloorLevel { get; set; }
        public int RoomNumber { get; set; }
        public string Section { get; set; } = string.Empty;
        public RoomStatus Status { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public string RoomDescription { get; set; } = string.Empty;
        public RoomType RoomType { get; set; }
    }


}
