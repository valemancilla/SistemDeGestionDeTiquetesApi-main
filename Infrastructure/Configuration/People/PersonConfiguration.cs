using Domain.Entities.People;
using Domain.ValueObjects.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.People;

public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("people");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TipoDocumentoId).IsRequired().HasColumnName("document_type_id");
        builder.Property(x => x.NumeroDocumento)
            .HasConversion(v => v.Value, v => DocumentNumber.Create(v))
            .IsRequired().HasMaxLength(30).HasColumnName("document_number");
        builder.Property(x => x.Nombres)
            .HasConversion(v => v.Value, v => PersonName.Create(v))
            .IsRequired().HasMaxLength(100).HasColumnName("first_name");
        builder.Property(x => x.Apellidos)
            .HasConversion(v => v.Value, v => PersonName.Create(v))
            .IsRequired().HasMaxLength(100).HasColumnName("last_name");
        builder.Property(x => x.FechaNacimiento).HasColumnName("birth_date");
        builder.Property(x => x.Genero).HasColumnType("char(1)").HasColumnName("gender");
        builder.Property(x => x.DireccionId).HasColumnName("address_id");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
        builder.Property(x => x.ActualizadoEn).IsRequired().HasColumnName("updated_at");
        builder.HasIndex(x => new { x.TipoDocumentoId, x.NumeroDocumento }).IsUnique().HasDatabaseName("IX_people_document_type_id_document_number");
        builder.HasIndex(x => x.DireccionId).HasDatabaseName("IX_people_address_id");
        builder.HasOne(x => x.TipoDocumento).WithMany().HasForeignKey(x => x.TipoDocumentoId);
        builder.HasOne(x => x.Direccion).WithMany().HasForeignKey(x => x.DireccionId);
    }
}
