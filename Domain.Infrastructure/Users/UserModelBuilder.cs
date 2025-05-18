using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.Users
{
    public static class UserModelBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
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
}
