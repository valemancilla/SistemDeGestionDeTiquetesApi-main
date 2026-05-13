using Domain.Entities.Tickets;
using Domain.ValueObjects.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Tickets;

public sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("tickets");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ReservaPasajeroId).IsRequired().HasColumnName("reservation_passenger_id");
        builder.Property(x => x.CodigoTiquete)
            .HasConversion(v => v.Value, v => TicketCode.Create(v))
            .IsRequired().HasMaxLength(30).HasColumnName("ticket_code");
        builder.Property(x => x.FechaEmision).IsRequired().HasColumnName("issued_at");
        builder.Property(x => x.EstadoTiqueteId).IsRequired().HasColumnName("ticket_status_id");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
        builder.Property(x => x.ActualizadoEn).IsRequired().HasColumnName("updated_at");
        builder.HasIndex(x => x.ReservaPasajeroId).IsUnique().HasDatabaseName("IX_tickets_reservation_passenger_id");
        builder.HasIndex(x => x.CodigoTiquete).IsUnique().HasDatabaseName("IX_tickets_ticket_code");
        builder.HasIndex(x => x.EstadoTiqueteId).HasDatabaseName("IX_tickets_ticket_status_id");
        builder.HasOne(x => x.ReservaPasajero).WithMany().HasForeignKey(x => x.ReservaPasajeroId);
        builder.HasOne(x => x.EstadoTiquete).WithMany().HasForeignKey(x => x.EstadoTiqueteId);
    }
}
