using Domain;

namespace RoomAsync.Web.Web.Login
{
    public class LoginApiClient(HttpClient httpClient, ILoggerService loggerService)
    {
        public async Task<UserContext?> LoginAsync(string userName, string password, CancellationToken cancellationToken = default)
        {
            loggerService.SetCorrelationId(Guid.NewGuid().ToString());
            loggerService.LogInformation("Logging in user : " + userName);


            var loginRequest = new LoginRequest
            {
                Username = userName,
                Password = password
            };

            var response = await httpClient.PostAsJsonAsync("/login", loginRequest, cancellationToken);


            if (response.IsSuccessStatusCode)
            {
                var userContext = await response.Content.ReadFromJsonAsync<UserContext>();
                return userContext;
            }
            else
            {
                return null;
            }

        }


        public class LoginRequest
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

    }
}
