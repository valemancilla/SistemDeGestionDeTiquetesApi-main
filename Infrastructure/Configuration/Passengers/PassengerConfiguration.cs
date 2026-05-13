using Domain.Entities.Passengers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Passengers;

public sealed class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.ToTable("passengers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PersonaId).IsRequired().HasColumnName("person_id");
        builder.Property(x => x.TipoPasajeroId).IsRequired().HasColumnName("passenger_type_id");
        builder.HasIndex(x => x.PersonaId).IsUnique().HasDatabaseName("IX_passengers_person_id");
        builder.HasIndex(x => x.TipoPasajeroId).HasDatabaseName("IX_passengers_passenger_type_id");
        builder.HasOne(x => x.Persona).WithMany().HasForeignKey(x => x.PersonaId);
        builder.HasOne(x => x.TipoPasajero).WithMany().HasForeignKey(x => x.TipoPasajeroId);
        builder.Ignore(x => x.CreatedAt);
    }
}
