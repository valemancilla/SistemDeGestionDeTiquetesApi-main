using Domain.Entities.People;
using Domain.ValueObjects.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.People;

public sealed class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
{
    public void Configure(EntityTypeBuilder<DocumentType> builder)
    {
        builder.ToTable("documenttypes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("Id");
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(255).HasColumnName("Name");
        builder.Property(x => x.Codigo)
            .HasConversion(v => v.Value, v => DocumentTypeCode.Create(v))
            .IsRequired().HasMaxLength(255).HasColumnName("Code");
        builder.HasIndex(x => x.Codigo).IsUnique().HasDatabaseName("IX_DocumentTypes_Code");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_DocumentTypes_Name");
        builder.Ignore(x => x.CreatedAt);
    }
}
