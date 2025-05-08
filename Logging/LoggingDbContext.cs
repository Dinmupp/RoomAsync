using Domain.Logs;
using Microsoft.EntityFrameworkCore;

namespace Logging
{
    public class LoggingDbContext : DbContext
    {
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options) { }
        public DbSet<LogEntry> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogEntry>(entity =>
            {
                entity.ToTable(nameof(Logs));
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Timestamp).IsRequired();
                entity.Property(e => e.Level).HasMaxLength(128).IsRequired();
                entity.Property(e => e.Message).IsRequired();
                entity.Property(e => e.MessageTemplate).IsRequired();
                entity.Property(e => e.Exception).IsRequired(false);
                entity.Property(e => e.CorrelationId).HasMaxLength(50).IsRequired(false);
                entity.Property(e => e.Properties).IsRequired(false);
            });
        }
    }

    public class LogEntry : ILogDataEntity
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