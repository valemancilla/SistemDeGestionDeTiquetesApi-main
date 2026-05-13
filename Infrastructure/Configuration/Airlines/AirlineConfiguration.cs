using Domain.Entities.Airlines;
using Domain.ValueObjects.Aviation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Airlines;

public sealed class AirlineConfiguration : IEntityTypeConfiguration<Airline>
{
    public void Configure(EntityTypeBuilder<Airline> builder)
    {
        builder.ToTable("airlines");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150).HasColumnName("name");
        builder.Property(x => x.CodigoIata)
            .HasConversion(v => v.Value, v => IataCode.Create(v))
            .IsRequired().HasMaxLength(3).HasColumnName("iata_code");
        builder.Property(x => x.PaisOrigenId).IsRequired().HasColumnName("origin_country_id");
        builder.Property(x => x.Activa).IsRequired().HasDefaultValue(true).HasColumnName("is_active");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
        builder.Property(x => x.ActualizadoEn).IsRequired().HasColumnName("updated_at");
        builder.HasIndex(x => x.PaisOrigenId).HasDatabaseName("IX_airlines_origin_country_id");
        builder.HasOne(x => x.PaisOrigen).WithMany().HasForeignKey(x => x.PaisOrigenId);
    }
}
