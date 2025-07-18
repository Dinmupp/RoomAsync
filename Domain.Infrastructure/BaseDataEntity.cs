namespace Domain.Infrastructure
{
    public abstract class BaseDataEntity
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }
    }
}
