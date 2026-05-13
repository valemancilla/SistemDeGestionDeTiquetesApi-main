using Domain.Entities.Auth;
using Domain.ValueObjects.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Auth;

public sealed class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("sessions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UsuarioId).IsRequired().HasColumnName("user_id");
        builder.Property(x => x.IniciadaEn).IsRequired().HasColumnName("started_at");
        builder.Property(x => x.CerradaEn).HasColumnName("closed_at");
        builder.Property(x => x.IpOrigen)
            .HasConversion(v => v == null ? null : v.Value, v => SessionIpAddress.CreateOptional(v))
            .HasMaxLength(45).HasColumnName("source_ip");
        builder.Property(x => x.Activa).IsRequired().HasDefaultValue(true).HasColumnName("is_active");
        builder.HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.UsuarioId);
        builder.HasIndex(x => x.UsuarioId).HasDatabaseName("IX_sessions_user_id");
        builder.Ignore(x => x.CreatedAt);
    }
}
