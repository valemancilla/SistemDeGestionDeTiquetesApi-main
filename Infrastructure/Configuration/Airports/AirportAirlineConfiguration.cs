using Domain.Entities.Airports;
using Domain.ValueObjects.Airports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Airports;

public sealed class AirportAirlineConfiguration : IEntityTypeConfiguration<AirportAirline>
{
    public void Configure(EntityTypeBuilder<AirportAirline> builder)
    {
        builder.ToTable("airportairline");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.AeropuertoId).IsRequired().HasColumnName("airport_id");
        builder.Property(x => x.AerolineaId).IsRequired().HasColumnName("airline_id");
        builder.Property(x => x.Terminal)
            .HasConversion(v => v == null ? null : v.Value, v => AirportTerminal.CreateOptional(v))
            .HasMaxLength(20).HasColumnName("terminal");
        builder.Property(x => x.FechaInicio).IsRequired().HasColumnName("start_date");
        builder.Property(x => x.FechaFin).HasColumnName("end_date");
        builder.Property(x => x.Activa).IsRequired().HasDefaultValue(true).HasColumnName("is_active");
        builder.HasIndex(x => new { x.AeropuertoId, x.AerolineaId }).IsUnique().HasDatabaseName("IX_airportairline_airport_id_airline_id");
        builder.HasIndex(x => x.AerolineaId).HasDatabaseName("IX_airportairline_airline_id");
        builder.HasOne(x => x.Aeropuerto).WithMany().HasForeignKey(x => x.AeropuertoId);
        builder.HasOne(x => x.Aerolinea).WithMany().HasForeignKey(x => x.AerolineaId);
        builder.Ignore(x => x.CreatedAt);
    }
}
