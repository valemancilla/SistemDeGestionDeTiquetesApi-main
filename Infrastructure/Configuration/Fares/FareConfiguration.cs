using Domain.Entities.Fares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Fares;

public sealed class FareConfiguration : IEntityTypeConfiguration<Fare>
{
    public void Configure(EntityTypeBuilder<Fare> builder)
    {
        builder.ToTable("fares");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.RutaId).IsRequired().HasColumnName("route_id");
        builder.Property(x => x.TipoCabinaId).IsRequired().HasColumnName("cabin_type_id");
        builder.Property(x => x.TipoPasajeroId).IsRequired().HasColumnName("passenger_type_id");
        builder.Property(x => x.TemporadaId).IsRequired().HasColumnName("season_id");
        builder.Property(x => x.PrecioBase).IsRequired().HasColumnType("numeric(18,2)").HasColumnName("base_price");
        builder.Property(x => x.VigenciaDesde).HasColumnName("valid_from");
        builder.Property(x => x.VigenciaHasta).HasColumnName("valid_until");
        builder.HasIndex(x => new { x.RutaId, x.TipoCabinaId, x.TipoPasajeroId, x.TemporadaId }).HasDatabaseName("IX_fares_route_id_cabin_type_id_passenger_type_id_season_id");
        builder.HasIndex(x => x.TemporadaId).HasDatabaseName("IX_fares_season_id");
        builder.HasIndex(x => x.TipoCabinaId).HasDatabaseName("IX_fares_cabin_type_id");
        builder.HasIndex(x => x.TipoPasajeroId).HasDatabaseName("IX_fares_passenger_type_id");
        builder.HasOne(x => x.Ruta).WithMany().HasForeignKey(x => x.RutaId);
        builder.HasOne(x => x.TipoCabina).WithMany().HasForeignKey(x => x.TipoCabinaId);
        builder.HasOne(x => x.TipoPasajero).WithMany().HasForeignKey(x => x.TipoPasajeroId);
        builder.HasOne(x => x.Temporada).WithMany().HasForeignKey(x => x.TemporadaId);
        builder.Ignore(x => x.CreatedAt);
    }
}
