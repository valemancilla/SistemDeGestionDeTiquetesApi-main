using Domain.Entities.Flights;
using Domain.ValueObjects.Aviation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Flights;

public sealed class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.ToTable("flights");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CodigoVuelo)
            .HasConversion(v => v.Value, v => FlightCode.Create(v))
            .IsRequired().HasMaxLength(10).HasColumnName("flight_code");
        builder.Property(x => x.AerolineaId).IsRequired().HasColumnName("airline_id");
        builder.Property(x => x.RutaId).IsRequired().HasColumnName("route_id");
        builder.Property(x => x.AeronaveId).IsRequired().HasColumnName("aircraft_id");
        builder.Property(x => x.FechaSalida).IsRequired().HasColumnName("departure_at");
        builder.Property(x => x.FechaLlegadaEstimada).IsRequired().HasColumnName("estimated_arrival_at");
        builder.Property(x => x.CapacidadTotal).IsRequired().HasColumnName("total_capacity");
        builder.Property(x => x.AsientosDisponibles).IsRequired().HasColumnName("available_seats");
        builder.Property(x => x.EstadoVueloId).IsRequired().HasColumnName("flight_status_id");
        builder.Property(x => x.ReprogramadoEn).HasColumnName("rescheduled_at");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
        builder.Property(x => x.ActualizadoEn).IsRequired().HasColumnName("updated_at");
        builder.HasIndex(x => x.CodigoVuelo).IsUnique().HasDatabaseName("IX_flights_flight_code");
        builder.HasIndex(x => x.AerolineaId).HasDatabaseName("IX_flights_airline_id");
        builder.HasIndex(x => x.AeronaveId).HasDatabaseName("IX_flights_aircraft_id");
        builder.HasIndex(x => x.EstadoVueloId).HasDatabaseName("IX_flights_flight_status_id");
        builder.HasIndex(x => x.RutaId).HasDatabaseName("IX_flights_route_id");
        builder.HasOne(x => x.Aerolinea).WithMany().HasForeignKey(x => x.AerolineaId);
        builder.HasOne(x => x.Ruta).WithMany().HasForeignKey(x => x.RutaId);
        builder.HasOne(x => x.Aeronave).WithMany().HasForeignKey(x => x.AeronaveId);
        builder.HasOne(x => x.EstadoVuelo).WithMany().HasForeignKey(x => x.EstadoVueloId);
    }
}
