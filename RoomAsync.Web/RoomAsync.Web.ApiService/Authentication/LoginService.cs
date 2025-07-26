using Domain;
using Domain.User.Driver;

namespace RoomAsync.Web.ApiService.Authentication
{
    public class LoginService
    {
        private readonly IUserDriverPort _userDriverPort;

        public LoginService(IUserDriverPort userDriverPort)
        {
            _userDriverPort = userDriverPort;
        }

        public async Task<UserContext?> LoginAsync(string username, string password, CancellationToken cancellation)
        {
            return await _userDriverPort.LoginUserAsync(username, password, cancellation);
        }
    }
}
