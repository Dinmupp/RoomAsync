namespace Domain.User
{
    public class UserEntity : IDataEntityExposer<IUserDataEntity>, IAggregateRoot
    {
        public string UserId => _data.UserId ?? string.Empty;
        public string Username => _data.Username ?? string.Empty;
        public string PasswordHash => _data.PasswordHash ?? string.Empty;
        public string Roles => _data.Roles ?? string.Empty;
        public string Claims => _data.Claims ?? string.Empty;

        private readonly IUserDataEntity _data;
        public UserEntity(IUserDataEntity data)
        {
            _data = data;
        }

        TDataEntity IDataEntityExposer<IUserDataEntity>.GetInstanceAs<TDataEntity>() => (TDataEntity)_data;
    }
}
