namespace Domain.User.UseCases
{
    public sealed class LoginUseCase
    {
        private readonly IOAuthAdapter _oauthAdapter;

        public LoginUseCase(IOAuthAdapter oauthAdapter)
        {
            _oauthAdapter = oauthAdapter;
        }

        public async Task<string> ExecuteAsync(string username, string password, CancellationToken cancellation = default)
        {
            var accessToken = await _oauthAdapter.AuthenticateAsync(username, password);
            //var session = await _oauthAdapter.CreateSessionAsync(accessToken);
            // Store session in repository or browser storage
            return accessToken;
        }
    }

}
