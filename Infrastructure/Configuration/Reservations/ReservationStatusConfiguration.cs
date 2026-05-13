using Domain.Entities.Reservations;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Reservations;

public sealed class ReservationStatusConfiguration : IEntityTypeConfiguration<ReservationStatus>
{
    public void Configure(EntityTypeBuilder<ReservationStatus> builder)
    {
        builder.ToTable(ReservationStatuses.TableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(ReservationStatuses.NombreMaxLength).HasColumnName("name");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_reservationstatus_name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = ReservationStatuses.CreadaId, Nombre = ReservationStatuses.CreadaNombre },
            new { Id = ReservationStatuses.ConfirmadaId, Nombre = ReservationStatuses.ConfirmadaNombre },
            new { Id = ReservationStatuses.CanceladaId, Nombre = ReservationStatuses.CanceladaNombre },
            new { Id = ReservationStatuses.CompletadaId, Nombre = ReservationStatuses.CompletadaNombre });
    }
}
