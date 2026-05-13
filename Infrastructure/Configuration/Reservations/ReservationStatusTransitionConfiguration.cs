using Domain.Entities.Reservations;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Reservations;

public sealed class ReservationStatusTransitionConfiguration : IEntityTypeConfiguration<ReservationStatusTransition>
{
    public void Configure(EntityTypeBuilder<ReservationStatusTransition> builder)
    {
        builder.ToTable("reservation_status_transitions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EstadoOrigenId).IsRequired().HasColumnName("source_status_id");
        builder.Property(x => x.EstadoDestinoId).IsRequired().HasColumnName("target_status_id");
        builder.HasIndex(x => new { x.EstadoOrigenId, x.EstadoDestinoId }).IsUnique().HasDatabaseName("IX_reservation_status_transitions_source_status_id_target_status_id");
        builder.HasIndex(x => x.EstadoDestinoId).HasDatabaseName("IX_reservation_status_transitions_target_status_id");
        builder.HasOne(x => x.EstadoOrigen).WithMany().HasForeignKey(x => x.EstadoOrigenId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.EstadoDestino).WithMany().HasForeignKey(x => x.EstadoDestinoId).OnDelete(DeleteBehavior.Restrict);
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = 1, EstadoOrigenId = ReservationStatuses.CreadaId, EstadoDestinoId = ReservationStatuses.ConfirmadaId },
            new { Id = 2, EstadoOrigenId = ReservationStatuses.ConfirmadaId, EstadoDestinoId = ReservationStatuses.CompletadaId },
            new { Id = 3, EstadoOrigenId = ReservationStatuses.CreadaId, EstadoDestinoId = ReservationStatuses.CanceladaId },
            new { Id = 4, EstadoOrigenId = ReservationStatuses.ConfirmadaId, EstadoDestinoId = ReservationStatuses.CanceladaId });
    }
}
