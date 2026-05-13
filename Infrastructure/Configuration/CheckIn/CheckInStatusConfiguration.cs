using Domain.Entities.CheckIn;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.CheckIn;

public sealed class CheckInStatusConfiguration : IEntityTypeConfiguration<CheckInStatus>
{
    public void Configure(EntityTypeBuilder<CheckInStatus> builder)
    {
        builder.ToTable(CheckInStatuses.TableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(CheckInStatuses.NombreMaxLength).HasColumnName("name");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_checkin_statuses_name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = CheckInStatuses.PendienteId, Nombre = CheckInStatuses.PendienteNombre },
            new { Id = CheckInStatuses.CompletadoId, Nombre = CheckInStatuses.CompletadoNombre },
            new { Id = CheckInStatuses.NoShowId, Nombre = CheckInStatuses.NoShowNombre });
    }
}
