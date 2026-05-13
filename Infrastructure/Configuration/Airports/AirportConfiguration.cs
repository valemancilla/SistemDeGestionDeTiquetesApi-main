using Domain.Entities.Airports;
using Domain.ValueObjects.Aviation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Airports;

public sealed class AirportConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        builder.ToTable("airports");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(150).HasColumnName("name");
        builder.Property(x => x.CodigoIata)
            .HasConversion(v => v.Value, v => IataCode.Create(v))
            .IsRequired().HasMaxLength(3).HasColumnName("iata_code");
        builder.Property(x => x.CodigoIcao)
            .HasConversion(
                v => v != null ? v.Value : null,
                v => string.IsNullOrEmpty(v) ? null : IcaoCode.Create(v))
            .HasMaxLength(4).HasColumnName("icao_code");
        builder.Property(x => x.CiudadId).IsRequired().HasColumnName("city_id");
        builder.HasIndex(x => x.CodigoIata).IsUnique().HasDatabaseName("IX_airports_iata_code");
        builder.HasIndex(x => x.CodigoIcao).IsUnique().HasDatabaseName("IX_airports_icao_code");
        builder.HasIndex(x => x.CiudadId).HasDatabaseName("IX_airports_city_id");
        builder.HasOne(x => x.Ciudad).WithMany().HasForeignKey(x => x.CiudadId);
        builder.Ignore(x => x.CreatedAt);
    }
}
