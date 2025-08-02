
using Domain;

namespace RoomAsync.Web.Web.Authentication
{
    public class AuthApiClient(HttpClient httpClient, ILoggerService loggerService)
    {

        public async Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
        {
            loggerService.SetCorrelationId(Guid.NewGuid().ToString());
            loggerService.LogInformation("Getting logged in user");


            var response = await httpClient.GetAsync("/me", cancellationToken);


            if (!response.IsSuccessStatusCode || response.Content.Headers.ContentLength == 0)
            {
                return string.Empty;
            }

            var userContext = await response.Content.ReadFromJsonAsync<UserContext>(cancellationToken: cancellationToken);

            return userContext?.Token ?? "";
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }
    }
}
