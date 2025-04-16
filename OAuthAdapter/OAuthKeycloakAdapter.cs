using Domain;
using Domain.Session;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace KeyCloakOAuthAdapter
{
    public class OAuthKeycloakAdapter : IOAuthAdapter
    {
        private readonly OAuthConfig _config;
        private readonly HttpClient _httpClient;

        public OAuthKeycloakAdapter(IOptions<OAuthConfig> config, HttpClient httpClient)
        {
            _config = config.Value;
            _httpClient = httpClient;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var response = await _httpClient.PostAsync($"{_config.Authority}{_config.TokenEndpoint}", new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("client_id", _config?.ClientId ?? string.Empty),
            new KeyValuePair<string, string>("client_secret", _config?.ClientSecret ?? string.Empty),
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password)
        }));

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(content);

            return tokenResponse?.AccessToken ?? string.Empty;
        }

        public async Task<Session> CreateSessionAsync(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.Authority}{_config.UserInfoEndpoint}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var userInfo = JsonSerializer.Deserialize<UserInfo>(content) ?? new UserInfo();
            userInfo.SessionId = Guid.NewGuid().ToString();
            userInfo.CreatedAt = DateTime.UtcNow;
            userInfo.ExpiresAt = DateTime.UtcNow.AddHours(1);
            return new Session(userInfo);
        }
    }

    public class UserInfo : IUserSessionDataEntity
    {
        public string SessionId { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Roles { get; set; }
        public string? Claims { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
