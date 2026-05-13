using Domain.Entities.People;
using Domain.ValueObjects.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.People;

public sealed class PersonPhoneConfiguration : IEntityTypeConfiguration<PersonPhone>
{
    public void Configure(EntityTypeBuilder<PersonPhone> builder)
    {
        builder.ToTable("personphones");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PersonaId).IsRequired().HasColumnName("person_id");
        builder.Property(x => x.CodigoTelefonoId).IsRequired().HasColumnName("phone_code_id");
        builder.Property(x => x.NumeroTelefono)
            .HasConversion(v => v.Value, v => PhoneNationalNumber.Create(v))
            .IsRequired().HasMaxLength(20).HasColumnName("phone_number");
        builder.Property(x => x.EsPrincipal).IsRequired().HasDefaultValue(false).HasColumnName("is_primary");
        builder.HasOne(x => x.Persona).WithMany().HasForeignKey(x => x.PersonaId);
        builder.HasOne(x => x.CodigoTelefono).WithMany().HasForeignKey(x => x.CodigoTelefonoId);
        builder.HasIndex(x => x.CodigoTelefonoId).HasDatabaseName("IX_personphones_phone_code_id");
        builder.HasIndex(x => x.PersonaId).HasDatabaseName("IX_personphones_person_id");
        builder.Ignore(x => x.CreatedAt);
    }
}
