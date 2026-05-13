using Domain.Entities.Aircraft;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Aircraft;

public sealed class AircraftManufacturerConfiguration : IEntityTypeConfiguration<AircraftManufacturer>
{
    public void Configure(EntityTypeBuilder<AircraftManufacturer> builder)
    {
        builder.ToTable("aircraftmanufacturers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100).HasColumnName("name");
        builder.Property(x => x.PaisId).IsRequired().HasColumnName("country_id");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_aircraftmanufacturers_name");
        builder.HasIndex(x => x.PaisId).HasDatabaseName("IX_aircraftmanufacturers_country_id");
        builder.HasOne(x => x.Pais).WithMany().HasForeignKey(x => x.PaisId);
        builder.Ignore(x => x.CreatedAt);
    }
}
