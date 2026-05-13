using Domain.Entities.Aircraft;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Aircraft;

public sealed class AircraftModelConfiguration : IEntityTypeConfiguration<AircraftModel>
{
    public void Configure(EntityTypeBuilder<AircraftModel> builder)
    {
        builder.ToTable("aircraftmodels");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FabricanteId).IsRequired().HasColumnName("manufacturer_id");
        builder.Property(x => x.NombreModelo).IsRequired().HasMaxLength(100).HasColumnName("model_name");
        builder.Property(x => x.CapacidadMaxima).IsRequired().HasColumnName("max_capacity");
        builder.Property(x => x.PesoMaxDespegueKg).HasColumnType("numeric(10,2)").HasColumnName("max_takeoff_weight_kg");
        builder.Property(x => x.ConsumoCombustibleKgH).HasColumnType("numeric(8,2)").HasColumnName("fuel_consumption_kg_h");
        builder.Property(x => x.VelocidadCruceroKmh).HasColumnName("cruise_speed_kmh");
        builder.Property(x => x.AltitudCruceroFt).HasColumnName("cruise_altitude_ft");
        builder.HasOne(x => x.Fabricante).WithMany().HasForeignKey(x => x.FabricanteId);
        builder.HasIndex(x => x.FabricanteId).HasDatabaseName("IX_aircraftmodels_manufacturer_id");
        builder.Ignore(x => x.CreatedAt);
    }
}
