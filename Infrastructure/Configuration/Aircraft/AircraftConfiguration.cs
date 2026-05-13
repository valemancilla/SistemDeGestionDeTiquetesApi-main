using Domain.Entities.Aircraft;
using Domain.ValueObjects.Aviation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Aircraft;

public sealed class AircraftConfiguration : IEntityTypeConfiguration<Domain.Entities.Aircraft.Aircraft>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Aircraft.Aircraft> builder)
    {
        builder.ToTable("aircraft");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ModeloId).IsRequired().HasColumnName("model_id");
        builder.Property(x => x.AerolineaId).IsRequired().HasColumnName("airline_id");
        builder.Property(x => x.Matricula)
            .HasConversion(v => v.Value, v => AircraftRegistration.Create(v))
            .IsRequired().HasMaxLength(20).HasColumnName("registration");
        builder.Property(x => x.FechaFabricacion).HasColumnName("manufacture_date");
        builder.Property(x => x.Activa).IsRequired().HasDefaultValue(true).HasColumnName("is_active");
        builder.HasIndex(x => x.Matricula).IsUnique().HasDatabaseName("IX_aircraft_registration");
        builder.HasIndex(x => x.AerolineaId).HasDatabaseName("IX_aircraft_airline_id");
        builder.HasIndex(x => x.ModeloId).HasDatabaseName("IX_aircraft_model_id");
        builder.HasOne(x => x.Modelo).WithMany().HasForeignKey(x => x.ModeloId);
        builder.HasOne(x => x.Aerolinea).WithMany().HasForeignKey(x => x.AerolineaId);
        builder.Ignore(x => x.CreatedAt);
    }
}
