using Domain.Entities.Flights;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Flights;

public sealed class FlightStateConfiguration : IEntityTypeConfiguration<FlightState>
{
    public void Configure(EntityTypeBuilder<FlightState> builder)
    {
        builder.ToTable(FlightStates.TableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(FlightStates.NombreMaxLength).HasColumnName("name");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_flight_statuses_name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = FlightStates.ProgramadoId, Nombre = FlightStates.ProgramadoNombre },
            new { Id = FlightStates.EnVueloId, Nombre = FlightStates.EnVueloNombre },
            new { Id = FlightStates.AterrizadoId, Nombre = FlightStates.AterrizadoNombre },
            new { Id = FlightStates.CanceladoId, Nombre = FlightStates.CanceladoNombre });
    }
}
