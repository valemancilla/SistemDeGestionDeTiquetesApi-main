using Domain.Entities.Invoices;
using Domain.ValueObjects.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Invoices;

public sealed class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("invoices");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ReservaId).IsRequired().HasColumnName("reservation_id");
        builder.Property(x => x.NumeroFactura)
            .HasConversion(v => v.Value, v => InvoiceNumber.Create(v))
            .IsRequired().HasMaxLength(30).HasColumnName("invoice_number");
        builder.Property(x => x.FechaEmision).IsRequired().HasColumnName("issued_at");
        builder.Property(x => x.Subtotal).IsRequired().HasColumnType("numeric(18,2)").HasDefaultValue(0m).HasColumnName("subtotal");
        builder.Property(x => x.Impuestos).IsRequired().HasColumnType("numeric(18,2)").HasDefaultValue(0m).HasColumnName("taxes");
        builder.Property(x => x.Total).IsRequired().HasColumnType("numeric(18,2)").HasDefaultValue(0m).HasColumnName("total");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
        builder.HasIndex(x => x.ReservaId).IsUnique().HasDatabaseName("IX_invoices_reservation_id");
        builder.HasIndex(x => x.NumeroFactura).IsUnique().HasDatabaseName("IX_invoices_invoice_number");
        builder.HasOne(x => x.Reserva).WithMany().HasForeignKey(x => x.ReservaId);
    }
}
