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
                entity.Property(e => e.LogLevel).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Message).IsRequired();
                entity.Property(e => e.Exception).IsRequired(false);
                entity.Property(e => e.CorrelationId).HasMaxLength(50).IsRequired(false);
                entity.Property(e => e.Context).IsRequired(false);
            });
        }
    }

    public class LogEntry
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? LogLevel { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
        public string? CorrelationId { get; set; }
        public string? Context { get; set; }
    }
}