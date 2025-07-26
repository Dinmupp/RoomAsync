
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace RoomAsync.Web.Web.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly ProtectedLocalStorage _localStorage;
        private const string TokenKey = "access_token";

        public AuthService(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task SaveTokenAsync(string token)
        {
            await _localStorage.SetAsync(TokenKey, token);
        }

        public async Task<string> GetTokenAsync()
        {
            var result = await _localStorage.GetAsync<string>(TokenKey);
            return result.Success ? result.Value ?? string.Empty : "";
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }

        public async Task LogoutAsync()
        {
            await _localStorage.DeleteAsync(TokenKey);
        }
    }
}
