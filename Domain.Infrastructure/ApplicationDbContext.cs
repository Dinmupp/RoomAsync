using Domain.Session;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<UserDataEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSession>(entity =>
            {
                entity.ToTable(nameof(UserSessions));
                entity.HasKey(e => e.SessionId);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.Username).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Roles).IsRequired();
                entity.Property(e => e.Claims).IsRequired(false);
                entity.Property(e => e.ExpiresAt).IsRequired(false);
                entity.Property(e => e.CreatedAt).IsRequired(false);
            });

            modelBuilder.Entity<UserDataEntity>(entity =>
            {
                entity.ToTable(nameof(Users));
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.Username).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Roles).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Claims).IsRequired(false);
            });
        }
    }

    public class UserSession : IUserSessionDataEntity
    {
        public string SessionId { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Roles { get; set; }
        public string? Claims { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }

    public class UserDataEntity : IUserDataEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Roles { get; set; } = string.Empty;
        public string Claims { get; set; } = string.Empty;
    }
}