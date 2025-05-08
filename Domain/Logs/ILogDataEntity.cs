namespace Domain.Logs
{
    public interface ILogDataEntity
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? MessageTemplate { get; set; }
        public string? Level { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Exception { get; set; }
        public string? CorrelationId { get; set; }
        public string? Properties { get; set; }
    }
}
