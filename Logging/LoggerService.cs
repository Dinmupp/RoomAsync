using Domain;
using Serilog;

namespace Logging
{
    public class LoggerService : ILoggerService
    {
        private static readonly AsyncLocal<string> CorrelationIdContext = new();

        private readonly ILogger _logger;

        public LoggerService()
        {
            _logger = Log.Logger; // Use the global Serilog logger
        }

        public void LogInformation(string message, params object[] args)
        {
            var correlationId = GetCorrelationId();
            _logger.Information("[CorrelationId: {CorrelationId}] " + message, correlationId, args);
        }

        public void LogError(string message, params object[] args)
        {
            var correlationId = GetCorrelationId();
            _logger.Error("[CorrelationId: {CorrelationId}] " + message, correlationId, args);
        }

        public string GetCorrelationId()
        {
            return CorrelationIdContext.Value ?? "N/A";
        }

        public void SetCorrelationId(string correlationId)
        {
            CorrelationIdContext.Value = correlationId;
        }
    }
}
