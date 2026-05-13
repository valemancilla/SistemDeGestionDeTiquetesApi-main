using Domain.Entities.Geography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Geography;

public sealed class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable("regions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100).HasColumnName("name");
        builder.Property(x => x.Tipo).IsRequired().HasMaxLength(30).HasColumnName("type");
        builder.Property(x => x.PaisId).IsRequired().HasColumnName("country_id");
        builder.HasIndex(x => x.PaisId).HasDatabaseName("IX_regions_country_id");
        builder.HasOne(x => x.Pais).WithMany().HasForeignKey(x => x.PaisId);
        builder.Ignore(x => x.CreatedAt);
    }
}
