namespace Domain.User
{
    public interface IUserDataEntity
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Roles { get; set; }
        public string Claims { get; set; }
    }
}
