using Domain.Entities.Flights;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Flights;

public sealed class FlightStatusTransitionConfiguration : IEntityTypeConfiguration<FlightStatusTransition>
{
    public void Configure(EntityTypeBuilder<FlightStatusTransition> builder)
    {
        builder.ToTable("flightstatustransitions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EstadoOrigenId).IsRequired().HasColumnName("source_status_id");
        builder.Property(x => x.EstadoDestinoId).IsRequired().HasColumnName("target_status_id");
        builder.HasIndex(x => new { x.EstadoOrigenId, x.EstadoDestinoId }).IsUnique().HasDatabaseName("IX_flightstatustransitions_source_status_id_target_status_id");
        builder.HasIndex(x => x.EstadoDestinoId).HasDatabaseName("IX_flightstatustransitions_target_status_id");
        builder.HasOne(x => x.EstadoOrigen).WithMany().HasForeignKey(x => x.EstadoOrigenId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.EstadoDestino).WithMany().HasForeignKey(x => x.EstadoDestinoId).OnDelete(DeleteBehavior.Restrict);
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = 1, EstadoOrigenId = FlightStates.ProgramadoId, EstadoDestinoId = FlightStates.EnVueloId },
            new { Id = 2, EstadoOrigenId = FlightStates.EnVueloId, EstadoDestinoId = FlightStates.AterrizadoId },
            new { Id = 3, EstadoOrigenId = FlightStates.ProgramadoId, EstadoDestinoId = FlightStates.CanceladoId },
            new { Id = 4, EstadoOrigenId = FlightStates.EnVueloId, EstadoDestinoId = FlightStates.CanceladoId });
    }
}
