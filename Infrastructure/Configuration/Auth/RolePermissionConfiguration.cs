using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Auth;

public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("rolepermissions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.RolId).IsRequired().HasColumnName("role_id");
        builder.Property(x => x.PermisoId).IsRequired().HasColumnName("permission_id");
        builder.HasIndex(x => new { x.RolId, x.PermisoId }).IsUnique().HasDatabaseName("IX_rolepermissions_role_id_permission_id");
        builder.HasIndex(x => x.PermisoId).HasDatabaseName("IX_rolepermissions_permission_id");
        builder.HasOne(x => x.Rol).WithMany().HasForeignKey(x => x.RolId);
        builder.HasOne(x => x.Permiso).WithMany().HasForeignKey(x => x.PermisoId);
        builder.Ignore(x => x.CreatedAt);
    }
}
