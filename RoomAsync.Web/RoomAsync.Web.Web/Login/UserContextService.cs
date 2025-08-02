using Domain;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace RoomAsync.Web.Web.Login
{

    public class UserContextService
    {
        private readonly ProtectedSessionStorage _sessionStorage;

        public UserContextService(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public UserContext? CurrentUser { get; set; }

        public async Task<bool> IsLoggedIn()
        {
            if (CurrentUser != null)
            {
                return true;
            }
            if (_sessionStorage == null)
            {
                return false;
            }
            var result = await _sessionStorage.GetAsync<UserContext>("UserContext");
            if (result.Success && result.Value != null)
            {
                CurrentUser = result.Value;
                return true;
            }

            return false;
        }
    }

}
