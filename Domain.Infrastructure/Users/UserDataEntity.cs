using Domain.User;

namespace Domain.Infrastructure.Users
{
    public class UserDataEntity : BaseDataEntity, IUserDataEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Roles { get; set; } = string.Empty;
        public string Claims { get; set; } = string.Empty;
    }
}
