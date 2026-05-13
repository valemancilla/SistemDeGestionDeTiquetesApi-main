using Domain.Common;
using Domain.Entities.Airports;

namespace Domain.Entities.Routes;

public sealed class RouteStop : BaseEntity<int>
{
    public int RutaId { get; private set; }
    public int AeropuertoEscalaId { get; private set; }
    public int Orden { get; private set; }
    public int DuracionEscalaMin { get; private set; }

    public FlightRoute? Ruta { get; private set; }
    public Airport? AeropuertoEscala { get; private set; }

    private RouteStop() { }

    public RouteStop(int rutaId, int aeropuertoEscalaId, int orden, int duracionEscalaMin = 0)
    {
        RutaId = rutaId;
        AeropuertoEscalaId = aeropuertoEscalaId;
        Orden = orden;
        DuracionEscalaMin = duracionEscalaMin;
    }

    public void Update(int orden, int duracionEscalaMin)
    {
        Orden = orden;
        DuracionEscalaMin = duracionEscalaMin;
    }
}
