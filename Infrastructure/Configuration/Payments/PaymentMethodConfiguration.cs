using Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Payments;

public sealed class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("paymentmethods");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TipoMedioPagoId).IsRequired().HasColumnName("payment_method_type_id");
        builder.Property(x => x.TipoTarjetaId).HasColumnName("card_type_id");
        builder.Property(x => x.EmisorTarjetaId).HasColumnName("card_issuer_id");
        builder.Property(x => x.NombreComercial).IsRequired().HasMaxLength(50).HasColumnName("commercial_name");
        builder.HasIndex(x => x.NombreComercial).IsUnique().HasDatabaseName("IX_paymentmethods_commercial_name");
        builder.HasIndex(x => x.EmisorTarjetaId).HasDatabaseName("IX_paymentmethods_card_issuer_id");
        builder.HasIndex(x => x.TipoMedioPagoId).HasDatabaseName("IX_paymentmethods_payment_method_type_id");
        builder.HasIndex(x => x.TipoTarjetaId).HasDatabaseName("IX_paymentmethods_card_type_id");
        builder.HasOne(x => x.TipoMedioPago).WithMany().HasForeignKey(x => x.TipoMedioPagoId);
        builder.HasOne(x => x.TipoTarjeta).WithMany().HasForeignKey(x => x.TipoTarjetaId);
        builder.HasOne(x => x.EmisorTarjeta).WithMany().HasForeignKey(x => x.EmisorTarjetaId);
        builder.Ignore(x => x.CreatedAt);
    }
}
