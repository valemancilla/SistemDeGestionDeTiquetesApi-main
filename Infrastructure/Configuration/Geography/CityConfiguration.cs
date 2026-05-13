using Domain.Entities.Geography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Geography;

public sealed class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("cities");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100).HasColumnName("name");
        builder.Property(x => x.RegionId).IsRequired().HasColumnName("region_id");
        builder.HasIndex(x => x.RegionId).HasDatabaseName("IX_cities_region_id");
        builder.HasOne(x => x.Region).WithMany().HasForeignKey(x => x.RegionId);
        builder.Ignore(x => x.CreatedAt);
    }
}
