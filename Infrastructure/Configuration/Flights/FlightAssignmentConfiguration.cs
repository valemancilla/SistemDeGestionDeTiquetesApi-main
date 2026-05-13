using Domain.Entities.Flights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Flights;

public sealed class FlightAssignmentConfiguration : IEntityTypeConfiguration<FlightAssignment>
{
    public void Configure(EntityTypeBuilder<FlightAssignment> builder)
    {
        builder.ToTable("flightassignments");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.VueloId).IsRequired().HasColumnName("flight_id");
        builder.Property(x => x.PersonalId).IsRequired().HasColumnName("staff_id");
        builder.Property(x => x.RolVueloId).IsRequired().HasColumnName("flight_role_id");
        builder.HasIndex(x => new { x.VueloId, x.PersonalId }).IsUnique().HasDatabaseName("IX_flightassignments_flight_id_staff_id");
        builder.HasIndex(x => x.PersonalId).HasDatabaseName("IX_flightassignments_staff_id");
        builder.HasIndex(x => x.RolVueloId).HasDatabaseName("IX_flightassignments_flight_role_id");
        builder.HasOne(x => x.Vuelo).WithMany().HasForeignKey(x => x.VueloId);
        builder.HasOne(x => x.Personal).WithMany().HasForeignKey(x => x.PersonalId);
        builder.HasOne(x => x.RolVuelo).WithMany().HasForeignKey(x => x.RolVueloId);
        builder.Ignore(x => x.CreatedAt);
    }
}
