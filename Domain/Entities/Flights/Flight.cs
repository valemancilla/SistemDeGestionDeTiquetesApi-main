using Domain.Common;
using Domain.Entities.Airlines;
using Domain.Entities.Routes;
using Domain.ValueObjects.Aviation;

namespace Domain.Entities.Flights;

public sealed class Flight : BaseEntity<int>
{
    public FlightCode CodigoVuelo { get; private set; } = default!;
    public int AerolineaId { get; private set; }
    public int RutaId { get; private set; }
    public int AeronaveId { get; private set; }
    public DateTime FechaSalida { get; private set; }
    public DateTime FechaLlegadaEstimada { get; private set; }
    public int CapacidadTotal { get; private set; }
    public int AsientosDisponibles { get; private set; }
    public int EstadoVueloId { get; private set; }
    public DateTime? ReprogramadoEn { get; private set; }
    public DateTime ActualizadoEn { get; private set; } = DateTime.UtcNow;

    public Airline? Aerolinea { get; private set; }
    public FlightRoute? Ruta { get; private set; }
    public Aircraft.Aircraft? Aeronave { get; private set; }
    public FlightState? EstadoVuelo { get; private set; }

    private Flight() { }

    public Flight(string codigoVuelo, int aerolineaId, int rutaId, int aeronaveid,
        DateTime fechaSalida, DateTime fechaLlegadaEstimada, int capacidadTotal, int estadoVueloId)
    {
        CodigoVuelo = FlightCode.Create(codigoVuelo);
        AerolineaId = aerolineaId;
        RutaId = rutaId;
        AeronaveId = aeronaveid;
        FechaSalida = fechaSalida;
        FechaLlegadaEstimada = fechaLlegadaEstimada;
        CapacidadTotal = capacidadTotal;
        AsientosDisponibles = capacidadTotal;
        EstadoVueloId = estadoVueloId;
    }

    public void Update(int aeronaveid, DateTime fechaSalida, DateTime fechaLlegadaEstimada)
    {
        AeronaveId = aeronaveid;
        FechaSalida = fechaSalida;
        FechaLlegadaEstimada = fechaLlegadaEstimada;
        ActualizadoEn = DateTime.UtcNow;
    }

    public void CambiarEstado(int nuevoEstadoId, DateTime? reprogramadoEn = null)
    {
        EstadoVueloId = nuevoEstadoId;
        ReprogramadoEn = reprogramadoEn;
        ActualizadoEn = DateTime.UtcNow;
    }

    public void ReservarAsiento()
    {
        if (AsientosDisponibles <= 0) throw new InvalidOperationException("No hay asientos disponibles.");
        AsientosDisponibles--;
        ActualizadoEn = DateTime.UtcNow;
    }

    public void LiberarAsiento()
    {
        if (AsientosDisponibles >= CapacidadTotal) throw new InvalidOperationException("Los asientos ya están al máximo.");
        AsientosDisponibles++;
        ActualizadoEn = DateTime.UtcNow;
    }
}
