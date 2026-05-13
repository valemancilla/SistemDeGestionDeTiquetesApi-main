using Domain.Common;
using Domain.Entities.Airports;

namespace Domain.Entities.Routes;

public sealed class FlightRoute : BaseEntity<int>
{
    public int AeropuertoOrigenId { get; private set; }
    public int AeropuertoDestinoId { get; private set; }
    public int? DistanciaKm { get; private set; }
    public int? DuracionEstimadaMin { get; private set; }

    public Airport? AeropuertoOrigen { get; private set; }
    public Airport? AeropuertoDestino { get; private set; }

    private FlightRoute() { }

    public FlightRoute(int aeropuertoOrigenId, int aeropuertoDestinoId,
        int? distanciaKm = null, int? duracionEstimadaMin = null)
    {
        AeropuertoOrigenId = aeropuertoOrigenId;
        AeropuertoDestinoId = aeropuertoDestinoId;
        DistanciaKm = distanciaKm;
        DuracionEstimadaMin = duracionEstimadaMin;
    }

    public void Update(int? distanciaKm, int? duracionEstimadaMin)
    {
        DistanciaKm = distanciaKm;
        DuracionEstimadaMin = duracionEstimadaMin;
    }
}
