using Domain.Entities.Maintenance;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Maintenance;

public sealed class MaintenanceTypeConfiguration : IEntityTypeConfiguration<MaintenanceType>
{
    public void Configure(EntityTypeBuilder<MaintenanceType> builder)
    {
        builder.ToTable("maintenancetypes", AirlinesDb.LegacySchema);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100).HasColumnName("nombre");
        builder.HasIndex(x => x.Nombre).IsUnique();
        builder.Ignore(x => x.CreatedAt);
    }
}
