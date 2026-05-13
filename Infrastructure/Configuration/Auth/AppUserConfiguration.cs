using Domain.Entities.Auth;
using Domain.ValueObjects.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Auth;

public sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Username)
            .HasConversion(v => v.Value, v => global::Domain.ValueObjects.Auth.Username.Create(v))
            .IsRequired().HasMaxLength(50).HasColumnName("username");
        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(255).HasColumnName("password_hash");
        builder.Property(x => x.PersonaId).HasColumnName("person_id");
        builder.Property(x => x.RolId).IsRequired().HasColumnName("role_id");
        builder.Property(x => x.Activo).IsRequired().HasDefaultValue(true).HasColumnName("is_active");
        builder.Property(x => x.UltimoAcceso).HasColumnName("last_access_at");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
        builder.Property(x => x.ActualizadoEn).IsRequired().HasColumnName("updated_at");
        builder.HasIndex(x => x.Username).IsUnique().HasDatabaseName("IX_users_username");
        builder.HasIndex(x => x.PersonaId).IsUnique().HasDatabaseName("IX_users_person_id");
        builder.HasIndex(x => x.RolId).HasDatabaseName("IX_users_role_id");
        builder.HasOne(x => x.Persona).WithMany().HasForeignKey(x => x.PersonaId);
        builder.HasOne(x => x.Rol).WithMany().HasForeignKey(x => x.RolId);
    }
}
