using Domain.Common;
using Domain.Entities.People;

namespace Domain.Entities.Clients;

public sealed class Client : BaseEntity<int>
{
    public int PersonaId { get; private set; }

    public Person? Persona { get; private set; }

    private Client() { }

    public Client(int personaId)
    {
        PersonaId = personaId;
    }
}
