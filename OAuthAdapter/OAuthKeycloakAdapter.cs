using Domain;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KeyCloakOAuthAdapter
{
    public class OAuthKeycloakAdapter : IOAuthAdapter
    {
        private readonly OAuthConfig _config;
        private readonly HttpClient _httpClient;
        private readonly UserContext _userContext;

        public OAuthKeycloakAdapter(IOptions<OAuthConfig> config, HttpClient httpClient, UserContext userContext)
        {
            _config = config.Value;
            _httpClient = httpClient;
            _userContext = userContext;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_config?.Authority}{_config?.TokenEndpoint}");
            var content1 = new FormUrlEncodedContent(new[]
            {
                 new KeyValuePair<string, string>("grant_type", "password"),
                 new KeyValuePair<string, string>("client_id", _config?.ClientId ?? string.Empty),
                 new KeyValuePair<string, string>("username", username),
                 new KeyValuePair<string, string>("password", password),
                 new KeyValuePair<string, string>("client_secret", _config?.ClientSecret ?? string.Empty),
                 new KeyValuePair<string, string>("scope", "openid")
            });

            request.Content = content1;

            var response1 = await client.SendAsync(request);
            var responseString = await response1.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<TokenResponse>(responseString);

            return token?.AccessToken ?? string.Empty;
        }


        public async Task<UserContext> CreateUserContextAsync(string accessToken, CancellationToken cancellation = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.Authority}{_config.UserInfoEndpoint}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request, cancellation);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync(cancellation);
            var userInfo = JsonSerializer.Deserialize<UserInfo>(content) ?? new UserInfo();

            // Populate UserContext
            _userContext.UserId = userInfo.Sub;
            _userContext.Username = userInfo.PreferredUsername;
            _userContext.Roles = userInfo.Roles;
            _userContext.SessionId = Guid.NewGuid().ToString();
            _userContext.CreatedAt = DateTime.UtcNow;
            _userContext.ExpiresAt = DateTime.UtcNow.AddHours(1);
            return _userContext;
        }
    }


    public class UserInfo
    {

        [JsonPropertyName("sub")]
        public string Sub { get; set; } = string.Empty;

        [JsonPropertyName("email_verified")]
        public bool EmailVerified { get; set; }

        [JsonPropertyName("roles")]
        public List<string> Roles { get; set; } = [];

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("preferred_username")]
        public string PreferredUsername { get; set; } = string.Empty;

        [JsonPropertyName("given_name")]
        public string GivenName { get; set; } = string.Empty;

        [JsonPropertyName("family_name")]
        public string FamilyName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

    }
}
