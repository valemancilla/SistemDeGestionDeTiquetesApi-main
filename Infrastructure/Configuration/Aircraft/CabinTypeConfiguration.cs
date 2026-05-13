using Domain.Entities.Aircraft;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Aircraft;

public sealed class CabinTypeConfiguration : IEntityTypeConfiguration<CabinType>
{
    public void Configure(EntityTypeBuilder<CabinType> builder)
    {
        builder.ToTable(CabinTypes.TableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever().HasColumnName("Id");
        builder.Property(x => x.Nombre).IsRequired().HasColumnType("text").HasColumnName("Name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = CabinTypes.EconomicaId, Nombre = CabinTypes.EconomicaNombre },
            new { Id = CabinTypes.PremiumEconomyId, Nombre = CabinTypes.PremiumEconomyNombre },
            new { Id = CabinTypes.BusinessId, Nombre = CabinTypes.BusinessNombre },
            new { Id = CabinTypes.PrimeraId, Nombre = CabinTypes.PrimeraNombre });
    }
}
