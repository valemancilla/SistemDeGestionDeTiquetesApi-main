using Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Reservations;

public sealed class ReservationPassengerConfiguration : IEntityTypeConfiguration<ReservationPassenger>
{
    public void Configure(EntityTypeBuilder<ReservationPassenger> builder)
    {
        builder.ToTable("reservationpassengers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ReservaVueloId).IsRequired().HasColumnName("reservation_flight_id");
        builder.Property(x => x.PasajeroId).IsRequired().HasColumnName("passenger_id");
        builder.HasIndex(x => new { x.ReservaVueloId, x.PasajeroId }).IsUnique().HasDatabaseName("IX_reservationpassengers_reservation_flight_id_passenger_id");
        builder.HasIndex(x => x.PasajeroId).HasDatabaseName("IX_reservationpassengers_passenger_id");
        builder.HasOne(x => x.ReservaVuelo).WithMany().HasForeignKey(x => x.ReservaVueloId);
        builder.HasOne(x => x.Pasajero).WithMany().HasForeignKey(x => x.PasajeroId);
        builder.Ignore(x => x.CreatedAt);
    }
}
