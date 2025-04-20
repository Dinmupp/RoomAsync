namespace Domain
{

    public class OAuthConfig
    {
        public string? Authority { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? TokenEndpoint { get; set; }
        public string? UserInfoEndpoint { get; set; }
    }

}
