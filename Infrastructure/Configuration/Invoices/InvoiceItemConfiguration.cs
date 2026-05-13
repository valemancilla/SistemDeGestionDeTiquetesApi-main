using Domain.Entities.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Invoices;

public sealed class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.ToTable("invoiceitems");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FacturaId).IsRequired().HasColumnName("invoice_id");
        builder.Property(x => x.TipoItemId).IsRequired().HasColumnName("item_type_id");
        builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(200).HasColumnName("description");
        builder.Property(x => x.Cantidad).IsRequired().HasDefaultValue(1).HasColumnName("quantity");
        builder.Property(x => x.PrecioUnitario).IsRequired().HasColumnType("numeric(18,2)").HasColumnName("unit_price");
        builder.Property(x => x.Subtotal).IsRequired().HasColumnType("numeric(18,2)").HasColumnName("subtotal");
        builder.Property(x => x.ReservaPasajeroId).HasColumnName("reservation_passenger_id");
        builder.HasOne(x => x.Factura).WithMany().HasForeignKey(x => x.FacturaId);
        builder.HasOne(x => x.TipoItem).WithMany().HasForeignKey(x => x.TipoItemId);
        builder.HasOne(x => x.ReservaPasajero).WithMany().HasForeignKey(x => x.ReservaPasajeroId);
        builder.HasIndex(x => x.FacturaId).HasDatabaseName("IX_invoiceitems_invoice_id");
        builder.HasIndex(x => x.ReservaPasajeroId).HasDatabaseName("IX_invoiceitems_reservation_passenger_id");
        builder.HasIndex(x => x.TipoItemId).HasDatabaseName("IX_invoiceitems_item_type_id");
        builder.Ignore(x => x.CreatedAt);
    }
}
