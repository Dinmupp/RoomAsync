using Domain.Infrastructure.ReservationHolder;
using Domain.Infrastructure.Reservations;
using Domain.Infrastructure.Rooms;
using Domain.Infrastructure.Users;
using Domain.Session;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<UserDataEntity> Users { get; set; }
        public DbSet<RoomDataEntity> Rooms { get; set; }
        public DbSet<ReservationDataEntity> Reservations { get; set; }
        public DbSet<ReservationHolderDataEntity> ReservationHolders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Ta bort tror jag 
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

            UserModelBuilder.Build(modelBuilder);

            ReservationsModelBuilder.Build(modelBuilder);

            RoomModelBuilder.Build(modelBuilder);
            ReservationHolderModelBuilder.Build(modelBuilder);
        }
    }

    public class UserSession : IUserSessionDataEntity
    {
        public string SessionId { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Roles { get; set; }
        public string? Claims { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? ExpiresAt { get; set; }
    }
}