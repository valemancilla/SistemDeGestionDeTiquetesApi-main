using Domain.Entities.People;
using Domain.ValueObjects.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.People;

public sealed class EmailDomainConfiguration : IEntityTypeConfiguration<EmailDomain>
{
    public void Configure(EntityTypeBuilder<EmailDomain> builder)
    {
        builder.ToTable("emaildomains");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Dominio)
            .HasConversion(v => v.Value, v => EmailHostname.Create(v))
            .IsRequired().HasMaxLength(100).HasColumnName("domain");
        builder.HasIndex(x => x.Dominio).IsUnique().HasDatabaseName("IX_emaildomains_domain");
        builder.Ignore(x => x.CreatedAt);
    }
}
