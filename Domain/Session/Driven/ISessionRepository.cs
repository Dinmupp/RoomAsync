namespace Domain.Session.Driven
{

    public interface ISessionRepository
    {
        Session FindBySessionId(string sessionId);
    }

}
