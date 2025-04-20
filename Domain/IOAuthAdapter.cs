namespace Domain
{
    public interface IOAuthAdapter
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task<Session.Session> CreateSessionAsync(string accessToken);
    }

}
