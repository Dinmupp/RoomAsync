using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.Reservations
{
    public static class ReservationsModelBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservationDataEntity>(entity =>
            {
                entity.ToTable(nameof(Reservations));
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ReservationId)
                    .HasConversion(
                        v => v.Value,
                        v => v)
                    .IsRequired();
                entity.Property(e => e.RoomId)
                .HasConversion(
                        v => v.Value,
                        v => v)
                    .IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();
                entity.Property(e => e.ReservationHolderId)
                .HasConversion(
                        v => v.Value,
                        v => v)
                    .IsRequired();
                entity.HasOne(e => e.Room)
                    .WithMany()
                    .HasForeignKey(e => e.RoomId)
                    .HasPrincipalKey(e => e.RoomId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.ReservationHolder)
                    .WithMany()
                    .HasForeignKey(e => e.ReservationHolderId)
                    .HasPrincipalKey(e => e.ReservationHolderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
