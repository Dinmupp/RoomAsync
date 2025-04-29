namespace Domain.User.UseCases
{
    public sealed class LoginUseCase
    {
        private readonly IOAuthAdapter _oauthAdapter;
        public LoginUseCase(IOAuthAdapter oauthAdapter)
        {
            _oauthAdapter = oauthAdapter;
        }

        public async Task<UserContext> ExecuteAsync(string username, string password, CancellationToken cancellation = default)
        {
            var accessToken = await _oauthAdapter.AuthenticateAsync(username, password);
            return await _oauthAdapter.CreateUserContextAsync(accessToken);
        }
    }

}
