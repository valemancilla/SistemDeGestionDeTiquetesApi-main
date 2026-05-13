using Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Payments;

public sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payments");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ReservaId).IsRequired().HasColumnName("reservation_id");
        builder.Property(x => x.Monto).IsRequired().HasColumnType("numeric(18,2)").HasColumnName("amount");
        builder.Property(x => x.FechaPago).IsRequired().HasColumnName("paid_at");
        builder.Property(x => x.EstadoPagoId).IsRequired().HasColumnName("payment_status_id");
        builder.Property(x => x.MetodoPagoId).IsRequired().HasColumnName("payment_method_id");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
        builder.Property(x => x.ActualizadoEn).IsRequired().HasColumnName("updated_at");
        builder.HasOne(x => x.Reserva).WithMany().HasForeignKey(x => x.ReservaId);
        builder.HasOne(x => x.EstadoPago).WithMany().HasForeignKey(x => x.EstadoPagoId);
        builder.HasOne(x => x.MetodoPago).WithMany().HasForeignKey(x => x.MetodoPagoId);
        builder.HasIndex(x => x.EstadoPagoId).HasDatabaseName("IX_payments_payment_status_id");
        builder.HasIndex(x => x.MetodoPagoId).HasDatabaseName("IX_payments_payment_method_id");
        builder.HasIndex(x => x.ReservaId).HasDatabaseName("IX_payments_reservation_id");
    }
}
