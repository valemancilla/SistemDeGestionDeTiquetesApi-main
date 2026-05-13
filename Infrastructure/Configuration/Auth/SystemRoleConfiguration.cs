using Domain.Entities.Auth;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Auth;

public sealed class SystemRoleConfiguration : IEntityTypeConfiguration<SystemRole>
{
    public void Configure(EntityTypeBuilder<SystemRole> builder)
    {
        builder.ToTable(SystemRolesCatalog.TableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(SystemRolesCatalog.NombreMaxLength).HasColumnName("name");
        builder.Property(x => x.Descripcion).HasMaxLength(SystemRolesCatalog.DescripcionMaxLength).HasColumnName("description");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_system_roles_name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = SystemRolesCatalog.AdministradorId, Nombre = SystemRolesCatalog.AdministradorNombre, Descripcion = SystemRolesCatalog.AdministradorDescripcion },
            new { Id = SystemRolesCatalog.OperadorId, Nombre = SystemRolesCatalog.OperadorNombre, Descripcion = SystemRolesCatalog.OperadorDescripcion },
            new { Id = SystemRolesCatalog.ClienteId, Nombre = SystemRolesCatalog.ClienteNombre, Descripcion = SystemRolesCatalog.ClienteDescripcion });
    }
}
