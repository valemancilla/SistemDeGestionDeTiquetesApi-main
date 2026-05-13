using Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Reservations;

public sealed class ReservationFlightConfiguration : IEntityTypeConfiguration<ReservationFlight>
{
    public void Configure(EntityTypeBuilder<ReservationFlight> builder)
    {
        builder.ToTable("reservationflights");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ReservaId).IsRequired().HasColumnName("reservation_id");
        builder.Property(x => x.VueloId).IsRequired().HasColumnName("flight_id");
        builder.Property(x => x.ValorParcial).IsRequired().HasColumnType("numeric(18,2)").HasDefaultValue(0m).HasColumnName("partial_amount");
        builder.HasIndex(x => new { x.ReservaId, x.VueloId }).IsUnique().HasDatabaseName("IX_reservationflights_reservation_id_flight_id");
        builder.HasIndex(x => x.VueloId).HasDatabaseName("IX_reservationflights_flight_id");
        builder.HasOne(x => x.Reserva).WithMany().HasForeignKey(x => x.ReservaId);
        builder.HasOne(x => x.Vuelo).WithMany().HasForeignKey(x => x.VueloId);
        builder.Ignore(x => x.CreatedAt);
    }
}
