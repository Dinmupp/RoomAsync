namespace Domain
{
    public interface IOAuthAdapter
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task<UserContext> CreateUserContextAsync(string accessToken, CancellationToken cancellation = default);
    }

}
