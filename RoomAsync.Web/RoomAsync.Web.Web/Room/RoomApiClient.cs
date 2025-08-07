using Domain;
using Domain.Room.Response;
using System.Text.Json;

namespace RoomAsync.Web.Web.Room
{
    public class RoomApiClient(HttpClient httpClient, ILoggerService loggerService)
    {
        public async Task<GetAllResponse> GetAllRooms(GetAllRoomsRequest request, CancellationToken cancellationToken = default)
        {
            loggerService.SetCorrelationId(Guid.NewGuid().ToString());
            loggerService.LogInformation("Getting all Rooms");

            var response = await httpClient.PostAsJsonAsync("/getallrooms", request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {

                var options = new JsonSerializerOptions
                {
                    Converters = { new RoomEntityJsonConverter() },
                    PropertyNameCaseInsensitive = true
                };


                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GetAllResponse>(json, options);

                //var result = await response.Content.ReadFromJsonAsync<GetAllResponse>();
                return result ?? new GetAllResponse();
            }
            else
            {
                return new GetAllResponse();
            }

        }
    }
    public class GetAllRoomsRequest
    {
        public string? RoomNumber { get; set; }
        public int? StartIndex { get; set; }
        public int? Count { get; set; }
        public string? SortBy { get; set; }
        public bool SortByAscending { get; set; }
    }
}
