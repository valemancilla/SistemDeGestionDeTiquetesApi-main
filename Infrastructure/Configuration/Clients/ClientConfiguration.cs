using Domain.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.Clients;

public sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PersonaId).IsRequired().HasColumnName("person_id");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
        builder.HasIndex(x => x.PersonaId).IsUnique().HasDatabaseName("IX_clients_person_id");
        builder.HasOne(x => x.Persona).WithMany().HasForeignKey(x => x.PersonaId);
    }
}
