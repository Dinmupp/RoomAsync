namespace Domain
{
    public interface ILoggerService
    {
        void LogInformation(string message, params object[] args);

        void LogError(string message, params object[] args);

        string GetCorrelationId();

        void SetCorrelationId(string correlationId);
    }
}
