using Domain.Entities.Reservations;
using Domain.ValueObjects.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Reservations;

public sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("reservations");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CodigoReserva)
            .HasConversion(v => v.Value, v => BookingCode.Create(v))
            .IsRequired().HasMaxLength(30).HasColumnName("booking_code");
        builder.Property(x => x.ClienteId).IsRequired().HasColumnName("client_id");
        builder.Property(x => x.FechaReserva).IsRequired().HasColumnName("booked_at");
        builder.Property(x => x.EstadoReservaId).IsRequired().HasColumnName("reservation_status_id");
        builder.Property(x => x.ValorTotal).IsRequired().HasColumnType("numeric(18,2)").HasColumnName("total_amount");
        builder.Property(x => x.VenceEn).HasColumnName("expires_at");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
        builder.Property(x => x.ActualizadoEn).IsRequired().HasColumnName("updated_at");
        builder.HasIndex(x => x.CodigoReserva).IsUnique().HasDatabaseName("IX_reservations_booking_code");
        builder.HasIndex(x => x.ClienteId).HasDatabaseName("IX_reservations_client_id");
        builder.HasIndex(x => x.EstadoReservaId).HasDatabaseName("IX_reservations_reservation_status_id");
        builder.HasOne(x => x.Cliente).WithMany().HasForeignKey(x => x.ClienteId);
        builder.HasOne(x => x.EstadoReserva).WithMany().HasForeignKey(x => x.EstadoReservaId);
    }
}
