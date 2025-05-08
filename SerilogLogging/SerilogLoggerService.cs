using Domain;
using Domain.Logs;
using Serilog;
using Serilog.Core;

namespace SerilogLogging
{
    public class SerilogLoggerService : ILoggerService
    {
        private static readonly AsyncLocal<string> CorrelationIdContext = new();
        private readonly ILogger _logger;

        public SerilogLoggerService()
        {
            _logger = Log.Logger; 
        }

        public void LogError(string message, params object[] args)
        {
            var correlationId = GetCorrelationId();
            _logger.Error("[CorrelationId: {CorrelationId}] " + message, correlationId, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            var correlationId = GetCorrelationId();
            _logger.Information("[CorrelationId: {CorrelationId}] " + message, correlationId, args);
        }
        public string GetCorrelationId()
        {
            return CorrelationIdContext.Value ?? "N/A";
        }

        public void SetCorrelationId(string correlationId)
        {
            CorrelationIdContext.Value = correlationId;
        }

        public async Task<IEnumerable<LogEntity>> GetLogs()
        {
            var logs = await _loggingDbContext.Logs.ToListAsync();
            return logs.Select(log => LogEntity.Create(log));
        }
}
