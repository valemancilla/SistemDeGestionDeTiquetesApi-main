using Domain.Entities.Routes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Routes;

public sealed class RouteStopConfiguration : IEntityTypeConfiguration<RouteStop>
{
    public void Configure(EntityTypeBuilder<RouteStop> builder)
    {
        builder.ToTable("routestops");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.RutaId).IsRequired().HasColumnName("route_id");
        builder.Property(x => x.AeropuertoEscalaId).IsRequired().HasColumnName("stopover_airport_id");
        builder.Property(x => x.Orden).IsRequired().HasColumnName("stop_order");
        builder.Property(x => x.DuracionEscalaMin).IsRequired().HasDefaultValue(0).HasColumnName("stopover_duration_min");
        builder.HasIndex(x => new { x.RutaId, x.Orden }).IsUnique().HasDatabaseName("IX_routestops_route_id_stop_order");
        builder.HasIndex(x => x.AeropuertoEscalaId).HasDatabaseName("IX_routestops_stopover_airport_id");
        builder.HasOne(x => x.Ruta).WithMany().HasForeignKey(x => x.RutaId);
        builder.HasOne(x => x.AeropuertoEscala).WithMany().HasForeignKey(x => x.AeropuertoEscalaId);
        builder.Ignore(x => x.CreatedAt);
    }
}
