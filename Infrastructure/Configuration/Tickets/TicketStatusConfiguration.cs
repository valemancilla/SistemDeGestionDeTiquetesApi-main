using Domain.Entities.Tickets;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Tickets;

public sealed class TicketStatusConfiguration : IEntityTypeConfiguration<TicketStatus>
{
    public void Configure(EntityTypeBuilder<TicketStatus> builder)
    {
        builder.ToTable(TicketStatuses.TableName);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(TicketStatuses.NombreMaxLength).HasColumnName("name");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_ticket_statuses_name");
        builder.Ignore(x => x.CreatedAt);

        builder.HasData(
            new { Id = TicketStatuses.EmitidoId, Nombre = TicketStatuses.EmitidoNombre },
            new { Id = TicketStatuses.UsadoId, Nombre = TicketStatuses.UsadoNombre },
            new { Id = TicketStatuses.CanceladoId, Nombre = TicketStatuses.CanceladoNombre },
            new { Id = TicketStatuses.ReembolsadoId, Nombre = TicketStatuses.ReembolsadoNombre });
    }
}
