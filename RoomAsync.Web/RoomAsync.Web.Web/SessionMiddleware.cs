using Domain;
using Domain.Session.Driven;

namespace RoomAsync.Web.Web
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserContext _userContext;
        private readonly ISessionRepository _sessionRepository;

        public SessionMiddleware(RequestDelegate next, UserContext userContext, ISessionRepository sessionRepository)
        {
            _next = next;
            _userContext = userContext;
            _sessionRepository = sessionRepository;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sessionId = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(sessionId))
            {
                var session = _sessionRepository.FindBySessionId(sessionId);
                if (session != null && !session.IsExpired())
                {
                    _userContext.SetCurrentSession(session);
                }
            }

            await _next(context);
        }
    }
}
