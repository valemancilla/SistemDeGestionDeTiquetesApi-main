using Domain.Entities.Flights;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Flights;

public sealed class SeatLocationTypeConfiguration : IEntityTypeConfiguration<SeatLocationType>
{
    public void Configure(EntityTypeBuilder<SeatLocationType> builder)
    {
        builder.ToTable(SeatLocationTypes.TableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(SeatLocationTypes.NombreMaxLength).HasColumnName("name");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_seat_location_types_name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = SeatLocationTypes.VentanaId, Nombre = SeatLocationTypes.VentanaNombre },
            new { Id = SeatLocationTypes.CentroId, Nombre = SeatLocationTypes.CentroNombre },
            new { Id = SeatLocationTypes.PasilloId, Nombre = SeatLocationTypes.PasilloNombre });
    }
}
