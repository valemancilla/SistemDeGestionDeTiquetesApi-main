using Domain.Common;
using Domain.Entities.Airlines;
using Domain.ValueObjects.Airports;

namespace Domain.Entities.Airports;

public sealed class AirportAirline : BaseEntity<int>
{
    public int AeropuertoId { get; private set; }
    public int AerolineaId { get; private set; }
    public AirportTerminal? Terminal { get; private set; }
    public DateOnly FechaInicio { get; private set; }
    public DateOnly? FechaFin { get; private set; }
    public bool Activa { get; private set; } = true;

    public Airport? Aeropuerto { get; private set; }
    public Airline? Aerolinea { get; private set; }

    private AirportAirline() { }

    public AirportAirline(int aeropuertoId, int aerolineaId, DateOnly fechaInicio,
        string? terminal = null, DateOnly? fechaFin = null)
    {
        AeropuertoId = aeropuertoId;
        AerolineaId = aerolineaId;
        FechaInicio = fechaInicio;
        Terminal = AirportTerminal.CreateOptional(terminal);
        FechaFin = fechaFin;
    }

    public void Update(string? terminal, DateOnly? fechaFin, bool activa)
    {
        Terminal = AirportTerminal.CreateOptional(terminal);
        FechaFin = fechaFin;
        Activa = activa;
    }
}
