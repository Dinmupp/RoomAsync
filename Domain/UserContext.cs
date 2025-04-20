namespace Domain
{
    public class UserContext
    {
        private Session.Session? _currentSession;

        public void SetCurrentSession(Session.Session session)
        {
            _currentSession = session;
        }

        public Session.Session? GetCurrentSession() => _currentSession;
    }
}
