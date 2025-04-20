namespace Domain.Session
{
    public class Session : IDataEntityExposer<IUserSessionDataEntity>, IAggregateRoot
    {
        public string SessionId => _data.SessionId;
        public string UserId => _data.UserId ?? string.Empty;
        public string Username => _data.Username ?? string.Empty;
        public List<string> Roles => _data.Roles?.Split(',').ToList() ?? new List<string>();
        public Dictionary<string, string> Claims { get; } = new Dictionary<string, string>();
        public DateTime? CreatedAt => _data.CreatedAt;
        public DateTime? ExpiresAt => _data.ExpiresAt;

        private readonly IUserSessionDataEntity _data;
        public Session(IUserSessionDataEntity data)
        {
            _data = data;
        }

        TDataEntity IDataEntityExposer<IUserSessionDataEntity>.GetInstanceAs<TDataEntity>() => (TDataEntity)_data;

        public bool IsExpired() => DateTime.UtcNow >= ExpiresAt;
    }

}
