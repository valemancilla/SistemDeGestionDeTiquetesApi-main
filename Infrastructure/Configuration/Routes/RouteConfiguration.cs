using Domain.Entities.Routes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Routes;

public sealed class RouteConfiguration : IEntityTypeConfiguration<FlightRoute>
{
    public void Configure(EntityTypeBuilder<FlightRoute> builder)
    {
        builder.ToTable("routes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.AeropuertoOrigenId).IsRequired().HasColumnName("origin_airport_id");
        builder.Property(x => x.AeropuertoDestinoId).IsRequired().HasColumnName("destination_airport_id");
        builder.Property(x => x.DistanciaKm).HasColumnName("distance_km");
        builder.Property(x => x.DuracionEstimadaMin).HasColumnName("estimated_duration_min");
        builder.HasIndex(x => new { x.AeropuertoOrigenId, x.AeropuertoDestinoId }).IsUnique().HasDatabaseName("IX_routes_origin_airport_id_destination_airport_id");
        builder.HasIndex(x => x.AeropuertoDestinoId).HasDatabaseName("IX_routes_destination_airport_id");
        builder.HasOne(x => x.AeropuertoOrigen).WithMany().HasForeignKey(x => x.AeropuertoOrigenId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.AeropuertoDestino).WithMany().HasForeignKey(x => x.AeropuertoDestinoId).OnDelete(DeleteBehavior.Restrict);
        builder.Ignore(x => x.CreatedAt);
    }
}
