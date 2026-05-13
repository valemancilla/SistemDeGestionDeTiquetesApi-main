using Domain.Entities.Staff;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Staff;

public sealed class AvailabilityStatusConfiguration : IEntityTypeConfiguration<AvailabilityStatus>
{
    public void Configure(EntityTypeBuilder<AvailabilityStatus> builder)
    {
        builder.ToTable("availability_statuses");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(AvailabilityStatuses.NombreMaxLength).HasColumnName("name");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_availability_statuses_name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = AvailabilityStatuses.DisponibleId, Nombre = AvailabilityStatuses.DisponibleNombre },
            new { Id = AvailabilityStatuses.NoDisponibleId, Nombre = AvailabilityStatuses.NoDisponibleNombre },
            new { Id = AvailabilityStatuses.EnLicenciaId, Nombre = AvailabilityStatuses.EnLicenciaNombre });
    }
}
