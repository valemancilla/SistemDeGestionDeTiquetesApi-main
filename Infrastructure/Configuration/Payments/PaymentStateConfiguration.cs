using Domain.Entities.Payments;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Payments;

public sealed class PaymentStateConfiguration : IEntityTypeConfiguration<PaymentState>
{
    public void Configure(EntityTypeBuilder<PaymentState> builder)
    {
        builder.ToTable(PaymentStates.TableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(PaymentStates.NombreMaxLength).HasColumnName("name");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_payment_statuses_name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = PaymentStates.PendienteId, Nombre = PaymentStates.PendienteNombre },
            new { Id = PaymentStates.PagadoId, Nombre = PaymentStates.PagadoNombre },
            new { Id = PaymentStates.RechazadoId, Nombre = PaymentStates.RechazadoNombre },
            new { Id = PaymentStates.ReembolsadoId, Nombre = PaymentStates.ReembolsadoNombre });
    }
}
