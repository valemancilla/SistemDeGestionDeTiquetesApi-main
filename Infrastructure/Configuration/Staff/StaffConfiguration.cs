using Domain.Entities.Staff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Staff;

public sealed class StaffConfiguration : IEntityTypeConfiguration<Domain.Entities.Staff.Staff>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Staff.Staff> builder)
    {
        builder.ToTable("staff");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PersonaId).IsRequired().HasColumnName("person_id");
        builder.Property(x => x.CargoId).IsRequired().HasColumnName("position_id");
        builder.Property(x => x.AerolineaId).HasColumnName("airline_id");
        builder.Property(x => x.AeropuertoId).HasColumnName("airport_id");
        builder.Property(x => x.FechaIngreso).IsRequired().HasColumnName("hire_date");
        builder.Property(x => x.Activo).IsRequired().HasDefaultValue(true).HasColumnName("is_active");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
        builder.Property(x => x.ActualizadoEn).IsRequired().HasColumnName("updated_at");
        builder.HasIndex(x => x.PersonaId).IsUnique().HasDatabaseName("IX_staff_person_id");
        builder.HasIndex(x => x.AerolineaId).HasDatabaseName("IX_staff_airline_id");
        builder.HasIndex(x => x.AeropuertoId).HasDatabaseName("IX_staff_airport_id");
        builder.HasIndex(x => x.CargoId).HasDatabaseName("IX_staff_position_id");
        builder.HasOne(x => x.Persona).WithMany().HasForeignKey(x => x.PersonaId);
        builder.HasOne(x => x.Cargo).WithMany().HasForeignKey(x => x.CargoId);
        builder.HasOne(x => x.Aerolinea).WithMany().HasForeignKey(x => x.AerolineaId);
        builder.HasOne(x => x.Aeropuerto).WithMany().HasForeignKey(x => x.AeropuertoId);
    }
}
