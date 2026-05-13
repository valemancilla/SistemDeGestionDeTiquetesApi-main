using Domain.Entities.People;
using Domain.ValueObjects.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.People;

public sealed class PersonEmailConfiguration : IEntityTypeConfiguration<PersonEmail>
{
    public void Configure(EntityTypeBuilder<PersonEmail> builder)
    {
        builder.ToTable("personemails");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PersonaId).IsRequired().HasColumnName("person_id");
        builder.Property(x => x.UsuarioEmail)
            .HasConversion(v => v.Value, v => EmailLocalPart.Create(v))
            .IsRequired().HasMaxLength(100).HasColumnName("email_username");
        builder.Property(x => x.DominioEmailId).IsRequired().HasColumnName("email_domain_id");
        builder.Property(x => x.EsPrincipal).IsRequired().HasDefaultValue(false).HasColumnName("is_primary");
        builder.HasOne(x => x.Persona).WithMany().HasForeignKey(x => x.PersonaId);
        builder.HasOne(x => x.DominioEmail).WithMany().HasForeignKey(x => x.DominioEmailId);
        builder.HasIndex(x => x.DominioEmailId).HasDatabaseName("IX_personemails_email_domain_id");
        builder.HasIndex(x => x.PersonaId).HasDatabaseName("IX_personemails_person_id");
        builder.Ignore(x => x.CreatedAt);
    }
}
