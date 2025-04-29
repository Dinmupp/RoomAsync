namespace Domain
{
    public class UserContext
    {
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public string SessionId { get; set; } = string.Empty;
        public string? Claims { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
