using Domain.Common;
using Domain.ValueObjects.People;

namespace Domain.Entities.People;

public sealed class EmailDomain : BaseEntity<int>
{
    public EmailHostname Dominio { get; private set; } = default!;

    private EmailDomain() { }

    public EmailDomain(string dominio)
    {
        Dominio = EmailHostname.Create(dominio);
    }

    public void Update(string dominio) => Dominio = EmailHostname.Create(dominio);
}
