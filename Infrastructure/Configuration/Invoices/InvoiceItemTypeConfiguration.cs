using Domain.Entities.Invoices;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Invoices;

public sealed class InvoiceItemTypeConfiguration : IEntityTypeConfiguration<InvoiceItemType>
{
    public void Configure(EntityTypeBuilder<InvoiceItemType> builder)
    {
        builder.ToTable(InvoiceItemTypes.TableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(InvoiceItemTypes.NombreMaxLength).HasColumnName("name");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_invoice_item_types_name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = InvoiceItemTypes.TarifaBaseId, Nombre = InvoiceItemTypes.TarifaBaseNombre },
            new { Id = InvoiceItemTypes.ImpuestosId, Nombre = InvoiceItemTypes.ImpuestosNombre },
            new { Id = InvoiceItemTypes.TasasAeroportuariasId, Nombre = InvoiceItemTypes.TasasAeroportuariasNombre },
            new { Id = InvoiceItemTypes.CargosServicioId, Nombre = InvoiceItemTypes.CargosServicioNombre });
    }
}
