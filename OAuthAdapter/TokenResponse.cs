using System.Text.Json.Serialization;

namespace OAuthAdapter
{

    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }

}
