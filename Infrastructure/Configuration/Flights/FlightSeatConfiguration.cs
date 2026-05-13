using Domain.Entities.Flights;
using Domain.ValueObjects.Aviation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Flights;

public sealed class FlightSeatConfiguration : IEntityTypeConfiguration<FlightSeat>
{
    public void Configure(EntityTypeBuilder<FlightSeat> builder)
    {
        builder.ToTable("flightseats");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.VueloId).IsRequired().HasColumnName("flight_id");
        builder.Property(x => x.CodigoAsiento)
            .HasConversion(v => v.Value, v => SeatCode.Create(v))
            .IsRequired().HasMaxLength(5).HasColumnName("seat_code");
        builder.Property(x => x.TipoCabinaId).IsRequired().HasColumnName("cabin_type_id");
        builder.Property(x => x.TipoUbicacionId).IsRequired().HasColumnName("seat_location_type_id");
        builder.Property(x => x.EstaOcupado).IsRequired().HasDefaultValue(false).HasColumnName("is_occupied");
        builder.HasIndex(x => new { x.VueloId, x.CodigoAsiento }).IsUnique().HasDatabaseName("IX_flightseats_flight_id_seat_code");
        builder.HasIndex(x => x.TipoCabinaId).HasDatabaseName("IX_flightseats_cabin_type_id");
        builder.HasIndex(x => x.TipoUbicacionId).HasDatabaseName("IX_flightseats_seat_location_type_id");
        builder.HasOne(x => x.Vuelo).WithMany().HasForeignKey(x => x.VueloId);
        builder.HasOne(x => x.TipoCabina).WithMany().HasForeignKey(x => x.TipoCabinaId);
        builder.HasOne(x => x.TipoUbicacion).WithMany().HasForeignKey(x => x.TipoUbicacionId);
        builder.Ignore(x => x.CreatedAt);
    }
}
