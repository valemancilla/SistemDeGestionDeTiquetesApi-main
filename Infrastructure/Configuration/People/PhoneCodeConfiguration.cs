using Domain.Entities.People;
using Domain.ValueObjects.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.People;

public sealed class PhoneCodeConfiguration : IEntityTypeConfiguration<PhoneCode>
{
    public void Configure(EntityTypeBuilder<PhoneCode> builder)
    {
        builder.ToTable("phonecodes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CodigoPais)
            .HasConversion(v => v.Value, v => PhoneCountryCode.Create(v))
            .IsRequired().HasMaxLength(5).HasColumnName("country_code");
        builder.Property(x => x.NombrePais).IsRequired().HasMaxLength(100).HasColumnName("country_name");
        builder.HasIndex(x => x.CodigoPais).IsUnique().HasDatabaseName("IX_phonecodes_country_code");
        builder.Ignore(x => x.CreatedAt);
    }
}
