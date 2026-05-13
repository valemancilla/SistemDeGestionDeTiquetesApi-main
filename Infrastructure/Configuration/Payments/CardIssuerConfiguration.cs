using Domain.Entities.Payments;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Payments;

public sealed class CardIssuerConfiguration : IEntityTypeConfiguration<CardIssuer>
{
    public void Configure(EntityTypeBuilder<CardIssuer> builder)
    {
        builder.ToTable(CardIssuers.TableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(CardIssuers.NombreMaxLength).HasColumnName("name");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_card_issuers_name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = CardIssuers.VisaId, Nombre = CardIssuers.VisaNombre },
            new { Id = CardIssuers.MasterCardId, Nombre = CardIssuers.MasterCardNombre },
            new { Id = CardIssuers.AmericanExpressId, Nombre = CardIssuers.AmericanExpressNombre });
    }
}
