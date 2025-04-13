using Domain;
using Microsoft.AspNetCore.Http;

namespace CorrelationIdMiddleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public CorrelationIdMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = Guid.NewGuid().ToString();
            context.Response.Headers.Add("X-Correlation-ID", correlationId);
            _loggerService.SetCorrelationId(correlationId);

            _loggerService.LogInformation("Request started at {Path}", context.Request.Path);

            await _next(context);

            _loggerService.LogInformation("Request ended at {Path}", context.Request.Path);
        }
    }
}
