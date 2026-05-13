using Domain.Entities.Fares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Fares;

public sealed class PassengerTypeConfiguration : IEntityTypeConfiguration<PassengerType>
{
    public void Configure(EntityTypeBuilder<PassengerType> builder)
    {
        builder.ToTable("passenger_types");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nombre).IsRequired().HasMaxLength(50).HasColumnName("name");
        builder.Property(x => x.EdadMin).HasColumnName("min_age");
        builder.Property(x => x.EdadMax).HasColumnName("max_age");
        builder.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_passenger_types_name");
        builder.Ignore(x => x.CreatedAt);
    }
}
