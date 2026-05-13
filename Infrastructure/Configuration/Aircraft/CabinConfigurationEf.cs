using Domain.Entities.Aircraft;
using Domain.ValueObjects.Aircraft;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Aircraft;

public sealed class CabinConfigurationEf : IEntityTypeConfiguration<CabinConfiguration>
{
    public void Configure(EntityTypeBuilder<CabinConfiguration> builder)
    {
        builder.ToTable("cabinconfiguration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.AeronaveId).IsRequired().HasColumnName("aircraft_id");
        builder.Property(x => x.TipoCabinaId).IsRequired().HasColumnName("cabin_type_id");
        builder.Property(x => x.FilaInicio).IsRequired().HasColumnName("start_row");
        builder.Property(x => x.FilaFin).IsRequired().HasColumnName("end_row");
        builder.Property(x => x.AsientosPorFila).IsRequired().HasColumnName("seats_per_row");
        builder.Property(x => x.LetrasAsientos)
            .HasConversion(v => v.Value, v => CabinSeatLetters.Create(v))
            .IsRequired().HasMaxLength(10).HasColumnName("seat_letters");
        builder.HasIndex(x => new { x.AeronaveId, x.TipoCabinaId }).IsUnique().HasDatabaseName("IX_cabinconfiguration_aircraft_id_cabin_type_id");
        builder.HasIndex(x => x.TipoCabinaId).HasDatabaseName("IX_cabinconfiguration_cabin_type_id");
        builder.HasOne(x => x.Aeronave).WithMany().HasForeignKey(x => x.AeronaveId);
        builder.HasOne(x => x.TipoCabina).WithMany().HasForeignKey(x => x.TipoCabinaId);
        builder.Ignore(x => x.CreatedAt);
    }
}
