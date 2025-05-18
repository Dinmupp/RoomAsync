namespace Domain.Session
{
    public interface IUserSessionDataEntity
    {
        public string SessionId { get; set; }
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Roles { get; set; }
        public string? Claims { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? ExpiresAt { get; set; }
    }
}
