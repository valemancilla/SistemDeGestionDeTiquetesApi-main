using Domain.Common;
using Domain.Entities.Airlines;
using Domain.Entities.Airports;
using Domain.Entities.People;

namespace Domain.Entities.Staff;

public sealed class Staff : BaseEntity<int>
{
    public int PersonaId { get; private set; }
    public int CargoId { get; private set; }
    public int? AerolineaId { get; private set; }
    public int? AeropuertoId { get; private set; }
    public DateOnly FechaIngreso { get; private set; }
    public bool Activo { get; private set; } = true;
    public DateTime ActualizadoEn { get; private set; } = DateTime.UtcNow;

    public Person? Persona { get; private set; }
    public StaffRole? Cargo { get; private set; }
    public Airline? Aerolinea { get; private set; }
    public Airport? Aeropuerto { get; private set; }

    private Staff() { }

    public Staff(int personaId, int cargoId, DateOnly fechaIngreso,
        int? aerolineaId = null, int? aeropuertoId = null)
    {
        PersonaId = personaId;
        CargoId = cargoId;
        FechaIngreso = fechaIngreso;
        AerolineaId = aerolineaId;
        AeropuertoId = aeropuertoId;
    }

    public void Update(int cargoId, int? aerolineaId, int? aeropuertoId, bool activo)
    {
        CargoId = cargoId;
        AerolineaId = aerolineaId;
        AeropuertoId = aeropuertoId;
        Activo = activo;
        ActualizadoEn = DateTime.UtcNow;
    }
}
