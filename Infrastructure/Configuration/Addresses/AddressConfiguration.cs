using Domain.Entities.Addresses;
using Domain.ValueObjects.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Addresses;

public sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("addresses");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TipoViaId).IsRequired().HasColumnName("road_type_id");
        builder.Property(x => x.NombreVia).IsRequired().HasMaxLength(100).HasColumnName("road_name");
        builder.Property(x => x.Numero).HasMaxLength(20).HasColumnName("number");
        builder.Property(x => x.Complemento).HasMaxLength(100).HasColumnName("complement");
        builder.Property(x => x.CiudadId).IsRequired().HasColumnName("city_id");
        builder.Property(x => x.CodigoPostal)
            .HasConversion(v => v == null ? null : v.Value, v => PostalCode.CreateOptional(v))
            .HasMaxLength(20).HasColumnName("postal_code");
        builder.HasIndex(x => x.CiudadId).HasDatabaseName("IX_addresses_city_id");
        builder.HasIndex(x => x.TipoViaId).HasDatabaseName("IX_addresses_road_type_id");
        builder.HasOne(x => x.TipoVia).WithMany().HasForeignKey(x => x.TipoViaId);
        builder.HasOne(x => x.Ciudad).WithMany().HasForeignKey(x => x.CiudadId);
        builder.Ignore(x => x.CreatedAt);
    }
}
