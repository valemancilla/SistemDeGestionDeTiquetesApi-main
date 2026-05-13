using Domain.Entities.Maintenance;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Maintenance;

public sealed class AircraftMaintenanceConfiguration : IEntityTypeConfiguration<AircraftMaintenance>
{
    public void Configure(EntityTypeBuilder<AircraftMaintenance> builder)
    {
        builder.ToTable("aircraftmaintenance", AirlinesDb.LegacySchema);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.AeronaveId).IsRequired().HasColumnName("aeronave_id");
        builder.Property(x => x.TipoMantenimientoId).IsRequired().HasColumnName("tipo_mantenimiento_id");
        builder.Property(x => x.FechaInicio).IsRequired().HasColumnName("fecha_inicio");
        builder.Property(x => x.FechaFin).HasColumnName("fecha_fin");
        builder.Property(x => x.Descripcion).HasMaxLength(255).HasColumnName("descripcion");
        builder.HasOne(x => x.Aeronave).WithMany().HasForeignKey(x => x.AeronaveId);
        builder.HasOne(x => x.TipoMantenimiento).WithMany().HasForeignKey(x => x.TipoMantenimientoId);
        builder.Ignore(x => x.CreatedAt);
    }
}
