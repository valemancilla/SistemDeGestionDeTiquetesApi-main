using Domain.Entities.Geography;
using Domain.ValueObjects.Geography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Geography;

public sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("countries");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100).HasColumnName("name");
        builder.Property(x => x.CodigoIso)
            .HasConversion(v => v.Value, v => IsoCountryCode.Create(v))
            .IsRequired().HasMaxLength(3).HasColumnName("iso_code");
        builder.Property(x => x.ContinenteId).IsRequired().HasColumnName("continent_id");
        builder.HasIndex(x => x.CodigoIso).IsUnique().HasDatabaseName("IX_countries_iso_code");
        builder.HasIndex(x => x.ContinenteId).HasDatabaseName("IX_countries_continent_id");
        builder.HasOne(x => x.Continente).WithMany().HasForeignKey(x => x.ContinenteId);
        builder.Ignore(x => x.CreatedAt);
    }
}
