using Domain.Common;
using Domain.Entities.Aircraft;
using Domain.Entities.Routes;

namespace Domain.Entities.Fares;

public sealed class Fare : BaseEntity<int>
{
    public int RutaId { get; private set; }
    public int TipoCabinaId { get; private set; }
    public int TipoPasajeroId { get; private set; }
    public int TemporadaId { get; private set; }
    public decimal PrecioBase { get; private set; }
    public DateOnly? VigenciaDesde { get; private set; }
    public DateOnly? VigenciaHasta { get; private set; }

    public FlightRoute? Ruta { get; private set; }
    public CabinType? TipoCabina { get; private set; }
    public PassengerType? TipoPasajero { get; private set; }
    public Season? Temporada { get; private set; }

    private Fare() { }

    public Fare(int rutaId, int tipoCabinaId, int tipoPasajeroId, int temporadaId, decimal precioBase,
        DateOnly? vigenciaDesde = null, DateOnly? vigenciaHasta = null)
    {
        RutaId = rutaId;
        TipoCabinaId = tipoCabinaId;
        TipoPasajeroId = tipoPasajeroId;
        TemporadaId = temporadaId;
        PrecioBase = precioBase;
        VigenciaDesde = vigenciaDesde;
        VigenciaHasta = vigenciaHasta;
    }

    public void Update(decimal precioBase, DateOnly? vigenciaDesde, DateOnly? vigenciaHasta)
    {
        PrecioBase = precioBase;
        VigenciaDesde = vigenciaDesde;
        VigenciaHasta = vigenciaHasta;
    }
}
