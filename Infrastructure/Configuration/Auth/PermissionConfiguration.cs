using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Auth;

public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(100).HasColumnName("name");
        builder.Property(x => x.Descripcion).HasMaxLength(200).HasColumnName("description");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_permissions_name");
        builder.Ignore(x => x.CreatedAt);
    }
}
