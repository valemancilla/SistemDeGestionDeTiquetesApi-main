using Domain.Entities.Geography;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Geography;

public sealed class ContinentConfiguration : IEntityTypeConfiguration<Continent>
{
    public void Configure(EntityTypeBuilder<Continent> builder)
    {
        builder.ToTable("continents");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever().HasColumnName("Id");
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(Continents.NombreMaxLength).HasColumnName("Name");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_continents_Name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = Continents.AmericaId, Nombre = Continents.AmericaNombre },
            new { Id = Continents.EuropaId, Nombre = Continents.EuropaNombre },
            new { Id = Continents.AsiaId, Nombre = Continents.AsiaNombre },
            new { Id = Continents.AfricaId, Nombre = Continents.AfricaNombre },
            new { Id = Continents.OceaniaId, Nombre = Continents.OceaniaNombre },
            new { Id = Continents.AntartidaId, Nombre = Continents.AntartidaNombre });
    }
}
