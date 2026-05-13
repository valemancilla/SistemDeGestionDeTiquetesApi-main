using Domain.Common;
using Domain.Entities.Fares;
using Domain.Entities.People;

namespace Domain.Entities.Passengers;

public sealed class Passenger : BaseEntity<int>
{
    public int PersonaId { get; private set; }
    public int TipoPasajeroId { get; private set; }

    public Person? Persona { get; private set; }
    public PassengerType? TipoPasajero { get; private set; }

    private Passenger() { }

    public Passenger(int personaId, int tipoPasajeroId)
    {
        PersonaId = personaId;
        TipoPasajeroId = tipoPasajeroId;
    }

    public void Update(int tipoPasajeroId) => TipoPasajeroId = tipoPasajeroId;
}
