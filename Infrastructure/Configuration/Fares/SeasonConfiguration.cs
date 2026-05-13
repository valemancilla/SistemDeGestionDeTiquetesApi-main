using Domain.Entities.Fares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Fares;

public sealed class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.ToTable("seasons");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(50).HasColumnName("name");
        builder.Property(x => x.Descripcion).HasMaxLength(150).HasColumnName("description");
        builder.Property(x => x.PrecioFactor).IsRequired().HasColumnType("numeric(5,4)").HasDefaultValue(1.0000m).HasColumnName("price_factor");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_seasons_name");
        builder.Ignore(x => x.CreatedAt);
    }
}
