using Domain.Entities.Payments;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Payments;

public sealed class PaymentMethodTypeConfiguration : IEntityTypeConfiguration<PaymentMethodType>
{
    public void Configure(EntityTypeBuilder<PaymentMethodType> builder)
    {
        builder.ToTable(PaymentMethodTypes.TableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(PaymentMethodTypes.NombreMaxLength).HasColumnName("name");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_payment_method_types_name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = PaymentMethodTypes.TarjetaCreditoId, Nombre = PaymentMethodTypes.TarjetaCreditoNombre },
            new { Id = PaymentMethodTypes.TarjetaDebitoId, Nombre = PaymentMethodTypes.TarjetaDebitoNombre },
            new { Id = PaymentMethodTypes.EfectivoId, Nombre = PaymentMethodTypes.EfectivoNombre },
            new { Id = PaymentMethodTypes.TransferenciaId, Nombre = PaymentMethodTypes.TransferenciaNombre });
    }
}
