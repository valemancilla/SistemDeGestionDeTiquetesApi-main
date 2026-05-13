using Domain.Entities.Staff;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Staff;

public sealed class StaffRoleConfiguration : IEntityTypeConfiguration<StaffRole>
{
    public void Configure(EntityTypeBuilder<StaffRole> builder)
    {
        builder.ToTable("staff_positions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(StaffRoles.NombreMaxLength).HasColumnName("name");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_staff_positions_name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = StaffRoles.ComandanteId, Nombre = StaffRoles.ComandanteNombre },
            new { Id = StaffRoles.CopilotoId, Nombre = StaffRoles.CopilotoNombre },
            new { Id = StaffRoles.IngenieroVueloId, Nombre = StaffRoles.IngenieroVueloNombre },
            new { Id = StaffRoles.SobrecargoId, Nombre = StaffRoles.SobrecargoNombre },
            new { Id = StaffRoles.AuxiliarCabinaId, Nombre = StaffRoles.AuxiliarCabinaNombre });
    }
}
