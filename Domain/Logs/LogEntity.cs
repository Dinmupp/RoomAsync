namespace Domain.Logs
{
    public class LogEntity
    {
        private readonly ILogDataEntity _data;
        private LogEntity(ILogDataEntity data)
        {
            _data = data;
        }
        public static LogEntity Create(ILogDataEntity data)
        {
            return new LogEntity(data);
        }
        public int Id => _data.Id;
        public string? Message => _data.Message;
        public string? MessageTemplate => _data.MessageTemplate;
        public string? Level => _data.Level;
        public DateTime Timestamp => _data.Timestamp;
        public string? Exception => _data.Exception;
        public string? CorrelationId => _data.CorrelationId;
        public string? Properties => _data.Properties;
    }
}
