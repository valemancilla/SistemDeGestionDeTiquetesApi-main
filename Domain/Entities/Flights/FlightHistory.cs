using Domain.Common;

namespace Domain.Entities.Flights;

public sealed class FlightHistory : BaseEntity<int>
{
    public int VueloId { get; private set; }
    public int EstadoAnteriorId { get; private set; }
    public int EstadoNuevoId { get; private set; }
    public int? CambiadoPor { get; private set; }
    public DateTime FechaCambio { get; private set; } = DateTime.UtcNow;
    public string? Observacion { get; private set; }

    public Flight? Vuelo { get; private set; }
    public FlightState? EstadoAnterior { get; private set; }
    public FlightState? EstadoNuevo { get; private set; }

    private FlightHistory() { }

    public FlightHistory(int vueloId, int estadoAnteriorId, int estadoNuevoId,
        int? cambiadoPor = null, string? observacion = null)
    {
        VueloId = vueloId;
        EstadoAnteriorId = estadoAnteriorId;
        EstadoNuevoId = estadoNuevoId;
        CambiadoPor = cambiadoPor;
        Observacion = observacion;
    }
}
