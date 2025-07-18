using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.TableReservations
{
    public static class TableReservationModelBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<TableReservationDataEntity>();

            entity.ToTable(nameof(TableReservations));
            entity.HasKey(e => e.Id);

            entity.Property(e => e.ReservationId)
                    .HasConversion(
                        v => v.Value,
                        v => v)
                    .IsRequired();

            entity.Property(e => e.StartDate)
                .IsRequired();

            entity.Property(e => e.EndDate)
                .IsRequired();

            entity.Property(e => e.ReservationHolderId)
                    .HasConversion(
                        v => v.Value,
                        v => v)
                    .IsRequired();

            entity.Property(e => e.NumberOfGuests)
                .IsRequired();

            entity.Property(e => e.Allergies)
                .HasMaxLength(256);

            entity.Property(e => e.BillToRoomNumber)
                .HasMaxLength(32);

            entity.Property(e => e.PaidAtCashier)
                .IsRequired();
        }
    }
}
