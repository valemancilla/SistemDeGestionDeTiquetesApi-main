using Domain.Entities.Flights;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Flights;

public sealed class FlightHistoryConfiguration : IEntityTypeConfiguration<FlightHistory>
{
    public void Configure(EntityTypeBuilder<FlightHistory> builder)
    {
        builder.ToTable("flighthistory", AirlinesDb.LegacySchema);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.VueloId).IsRequired().HasColumnName("vuelo_id");
        builder.Property(x => x.EstadoAnteriorId).IsRequired().HasColumnName("estado_anterior_id");
        builder.Property(x => x.EstadoNuevoId).IsRequired().HasColumnName("estado_nuevo_id");
        builder.Property(x => x.CambiadoPor).HasColumnName("cambiado_por");
        builder.Property(x => x.FechaCambio).IsRequired().HasColumnName("fecha_cambio");
        builder.Property(x => x.Observacion).HasMaxLength(255).HasColumnName("observacion");
        builder.HasOne(x => x.Vuelo).WithMany().HasForeignKey(x => x.VueloId);
        builder.HasOne(x => x.EstadoAnterior).WithMany().HasForeignKey(x => x.EstadoAnteriorId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.EstadoNuevo).WithMany().HasForeignKey(x => x.EstadoNuevoId).OnDelete(DeleteBehavior.Restrict);
        builder.Ignore(x => x.CreatedAt);
    }
}
