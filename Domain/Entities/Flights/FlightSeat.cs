using Domain.Common;
using Domain.Entities.Aircraft;
using Domain.ValueObjects.Aviation;

namespace Domain.Entities.Flights;

public sealed class FlightSeat : BaseEntity<int>
{
    public int VueloId { get; private set; }
    public SeatCode CodigoAsiento { get; private set; } = default!;
    public int TipoCabinaId { get; private set; }
    public int TipoUbicacionId { get; private set; }
    public bool EstaOcupado { get; private set; }

    public Flight? Vuelo { get; private set; }
    public CabinType? TipoCabina { get; private set; }
    public SeatLocationType? TipoUbicacion { get; private set; }

    private FlightSeat() { }

    public FlightSeat(int vueloId, string codigoAsiento, int tipoCabinaId, int tipoUbicacionId)
    {
        VueloId = vueloId;
        CodigoAsiento = SeatCode.Create(codigoAsiento);
        TipoCabinaId = tipoCabinaId;
        TipoUbicacionId = tipoUbicacionId;
    }

    public void Ocupar() => EstaOcupado = true;
    public void Liberar() => EstaOcupado = false;
}
