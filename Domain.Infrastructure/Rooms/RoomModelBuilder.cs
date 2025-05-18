using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.Rooms
{
    public static class RoomModelBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomDataEntity>(entity =>
            {
                entity.ToTable(nameof(Rooms));
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RoomId).HasConversion(v => v.Value, v => v).IsRequired();
                entity.HasAlternateKey(e => e.RoomId);
                entity.Property(e => e.FloorLevel).IsRequired();
                entity.Property(e => e.RoomNumber).IsRequired();
                entity.Property(e => e.Section).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.RoomName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.RoomDescription).HasMaxLength(500).IsRequired(false);
                entity.Property(e => e.RoomType).IsRequired();
            });
        }
    }
}
