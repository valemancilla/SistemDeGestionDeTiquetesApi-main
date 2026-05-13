using Domain.Entities.Addresses;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Addresses;

public sealed class RoadTypeConfiguration : IEntityTypeConfiguration<RoadType>
{
    public void Configure(EntityTypeBuilder<RoadType> builder)
    {
        builder.ToTable("roadtypes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever().HasColumnName("Id");
        builder.Property(x => x.Nombre).IsRequired().HasColumnType("text").HasColumnName("Name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = RoadTypes.CalleId, Nombre = RoadTypes.CalleNombre },
            new { Id = RoadTypes.CarreraId, Nombre = RoadTypes.CarreraNombre },
            new { Id = RoadTypes.AvenidaId, Nombre = RoadTypes.AvenidaNombre },
            new { Id = RoadTypes.DiagonalId, Nombre = RoadTypes.DiagonalNombre },
            new { Id = RoadTypes.TransversalId, Nombre = RoadTypes.TransversalNombre },
            new { Id = RoadTypes.CircunvalarId, Nombre = RoadTypes.CircunvalarNombre });
    }
}
