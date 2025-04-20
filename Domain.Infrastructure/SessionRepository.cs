using Domain.Extensions;
using Domain.Session;
using Domain.Session.Driven;

namespace Domain.Infrastructure
{
    public class SessionRepository : ISessionRepository
    {
        private readonly Dictionary<string, UserSession> _sessions = new();

        public Session.Session FindBySessionId(string sessionId)
        {
            _sessions.TryGetValue(sessionId, out var session);
            return new Session.Session(session ?? null!);
        }

        public void AddSession(Session.Session session)
        {
            var originalDataEntity = session.ExposeDataEntity<IUserSessionDataEntity>().GetInstanceAs<UserSession>();
            _sessions[session.SessionId] = originalDataEntity;
        }
    }

}
