using Domain;
using Domain.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Logging
{
    public class LoggerService : ILoggerService
    {
        private static readonly AsyncLocal<string> CorrelationIdContext = new();
        private readonly LoggingDbContext _loggingDbContext;

        public LoggerService(LoggingDbContext loggingDbContext)
        {
            _loggingDbContext = loggingDbContext;
        }

        public void LogInformation(string message, params object[] args)
        {
            var correlationId = GetCorrelationId();
            SaveLog(message, args, correlationId, LogLevel.Information);
        }

        private void SaveLog(string message, object[] args, string correlationId, LogLevel logLevel)
        {
            _loggingDbContext.Logs.Add(new LogEntry
            {
                CorrelationId = correlationId,
                Message = message,
                Properties = args.Length > 0 ? string.Join(", ", args) : null,
                Level = logLevel.ToString(),
                Timestamp = DateTime.UtcNow,
                Exception = args.Length > 0 && args[0] is Exception ex ? ex.ToString() : null,
                MessageTemplate = message
            });
            _loggingDbContext.SaveChanges();
        }

        public void LogError(string message, params object[] args)
        {
            var correlationId = GetCorrelationId();
            SaveLog(message, args, correlationId, LogLevel.Error);
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
            return logs.Select(LogEntity.Create);
        }
    }
}
