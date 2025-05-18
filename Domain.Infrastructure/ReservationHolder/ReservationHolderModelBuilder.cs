using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.ReservationHolder
{
    internal class ReservationHolderModelBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservationHolderDataEntity>(entity =>
            {
                entity.ToTable(nameof(ReservationHolder));
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ReservationHolderId)
                    .HasConversion(v => v.Value, v => v)
                    .IsRequired();
                entity.HasAlternateKey(e => e.ReservationHolderId);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Phone).HasMaxLength(100).IsRequired();
            });
        }
    }
}
