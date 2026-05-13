using Domain.ValueObjects.CheckIn;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.CheckIn;

public sealed class CheckInConfiguration : IEntityTypeConfiguration<Domain.Entities.CheckIn.CheckIn>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.CheckIn.CheckIn> builder)
    {
        builder.ToTable("checkins");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TiqueteId).IsRequired().HasColumnName("ticket_id");
        builder.Property(x => x.PersonalId).IsRequired().HasColumnName("staff_id");
        builder.Property(x => x.AsientoVueloId).IsRequired().HasColumnName("flight_seat_id");
        builder.Property(x => x.FechaCheckin).IsRequired().HasColumnName("checkin_at");
        builder.Property(x => x.EstadoCheckinId).IsRequired().HasColumnName("checkin_status_id");
        builder.Property(x => x.NumeroTarjetaEmbarque)
            .HasConversion(v => v.Value, v => BoardingPassNumber.Create(v))
            .IsRequired().HasMaxLength(20).HasColumnName("boarding_pass_number");
        builder.Property(x => x.EquipajeBodega).IsRequired().HasDefaultValue(false).HasColumnName("has_checked_baggage");
        builder.Property(x => x.PesoEquipajeKg).HasColumnType("numeric(5,2)").HasDefaultValue(0m).HasColumnName("baggage_weight_kg");
        builder.HasIndex(x => x.TiqueteId).IsUnique().HasDatabaseName("IX_checkins_ticket_id");
        builder.HasIndex(x => x.AsientoVueloId).IsUnique().HasDatabaseName("IX_checkins_flight_seat_id");
        builder.HasIndex(x => x.NumeroTarjetaEmbarque).IsUnique().HasDatabaseName("IX_checkins_boarding_pass_number");
        builder.HasIndex(x => x.EstadoCheckinId).HasDatabaseName("IX_checkins_checkin_status_id");
        builder.HasIndex(x => x.PersonalId).HasDatabaseName("IX_checkins_staff_id");
        builder.HasOne(x => x.Tiquete).WithMany().HasForeignKey(x => x.TiqueteId);
        builder.HasOne(x => x.Personal).WithMany().HasForeignKey(x => x.PersonalId);
        builder.HasOne(x => x.AsientoVuelo).WithMany().HasForeignKey(x => x.AsientoVueloId);
        builder.HasOne(x => x.EstadoCheckin).WithMany().HasForeignKey(x => x.EstadoCheckinId);
        builder.Ignore(x => x.CreatedAt);
    }
}
