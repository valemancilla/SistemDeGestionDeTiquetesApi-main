using Domain.Common;
using Domain.Entities.Airlines;
using Domain.ValueObjects.Aviation;

namespace Domain.Entities.Aircraft;

public sealed class Aircraft : BaseEntity<int>
{
    public int ModeloId { get; private set; }
    public int AerolineaId { get; private set; }
    public AircraftRegistration Matricula { get; private set; } = default!;
    public DateOnly? FechaFabricacion { get; private set; }
    public bool Activa { get; private set; } = true;

    public AircraftModel? Modelo { get; private set; }
    public Airline? Aerolinea { get; private set; }

    private Aircraft() { }

    public Aircraft(int modeloId, int aerolineaId, string matricula, DateOnly? fechaFabricacion = null)
    {
        ModeloId = modeloId;
        AerolineaId = aerolineaId;
        Matricula = AircraftRegistration.Create(matricula);
        FechaFabricacion = fechaFabricacion;
    }

    public void Update(int modeloId, int aerolineaId, DateOnly? fechaFabricacion, bool activa)
    {
        ModeloId = modeloId;
        AerolineaId = aerolineaId;
        FechaFabricacion = fechaFabricacion;
        Activa = activa;
    }

    public void SetActiva(bool activa) => Activa = activa;
}
