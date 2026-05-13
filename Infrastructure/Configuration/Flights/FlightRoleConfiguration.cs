using Domain.Entities.Flights;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Flights;

public sealed class FlightRoleConfiguration : IEntityTypeConfiguration<FlightRole>
{
    public void Configure(EntityTypeBuilder<FlightRole> builder)
    {
        builder.ToTable(FlightRoles.TableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(FlightRoles.NombreMaxLength).HasColumnName("name");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_flight_roles_name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = FlightRoles.PilotoComandanteId, Nombre = FlightRoles.PilotoComandanteNombre },
            new { Id = FlightRoles.PilotoCopilotoId, Nombre = FlightRoles.PilotoCopilotoNombre },
            new { Id = FlightRoles.JefeCabinaId, Nombre = FlightRoles.JefeCabinaNombre },
            new { Id = FlightRoles.TripulanteCabinaId, Nombre = FlightRoles.TripulanteCabinaNombre });
    }
}
