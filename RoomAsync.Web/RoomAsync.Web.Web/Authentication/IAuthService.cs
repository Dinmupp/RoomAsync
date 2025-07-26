namespace RoomAsync.Web.Web.Authentication
{
    public interface IAuthService
    {
        Task SaveTokenAsync(string token);

        Task<string> GetTokenAsync();

        Task<bool> IsLoggedInAsync();

        Task LogoutAsync();
    }
}
