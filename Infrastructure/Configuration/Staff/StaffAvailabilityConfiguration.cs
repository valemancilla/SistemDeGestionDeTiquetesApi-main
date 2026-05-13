using Domain.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Staff;

public sealed class StaffAvailabilityConfiguration : IEntityTypeConfiguration<StaffAvailability>
{
    public void Configure(EntityTypeBuilder<StaffAvailability> builder)
    {
        builder.ToTable("staffavailability");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PersonalId).IsRequired().HasColumnName("staff_id");
        builder.Property(x => x.EstadoDisponibilidadId).IsRequired().HasColumnName("availability_status_id");
        builder.Property(x => x.FechaInicio).IsRequired().HasColumnName("start_date");
        builder.Property(x => x.FechaFin).IsRequired().HasColumnName("end_date");
        builder.Property(x => x.Observacion).HasMaxLength(255).HasColumnName("notes");
        builder.HasIndex(x => x.EstadoDisponibilidadId).HasDatabaseName("IX_staffavailability_availability_status_id");
        builder.HasIndex(x => x.PersonalId).HasDatabaseName("IX_staffavailability_staff_id");
        builder.HasOne(x => x.Personal).WithMany().HasForeignKey(x => x.PersonalId);
        builder.HasOne(x => x.EstadoDisponibilidad).WithMany().HasForeignKey(x => x.EstadoDisponibilidadId);
        builder.Ignore(x => x.CreatedAt);
    }
}
