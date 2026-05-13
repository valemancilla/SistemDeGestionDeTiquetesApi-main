using Domain.Entities.Payments;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Payments;

public sealed class CardTypeConfiguration : IEntityTypeConfiguration<CardType>
{
    public void Configure(EntityTypeBuilder<CardType> builder)
    {
        builder.ToTable(CardTypes.TableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(CardTypes.NombreMaxLength).HasColumnName("name");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_card_types_name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = CardTypes.CreditoId, Nombre = CardTypes.CreditoNombre },
            new { Id = CardTypes.DebitoId, Nombre = CardTypes.DebitoNombre },
            new { Id = CardTypes.PrepagoId, Nombre = CardTypes.PrepagoNombre });
    }
}
