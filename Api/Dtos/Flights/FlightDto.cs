namespace Api.Dtos.Flights;

public sealed record FlightDto(
    int Id,
    string CodigoVuelo,
    int AerolineaId,
    string? NombreAerolinea,
    int RutaId,
    int AeronaveId,
    DateTime FechaSalida,
    DateTime FechaLlegadaEstimada,
    int CapacidadTotal,
    int AsientosDisponibles,
    int EstadoVueloId,
    string? EstadoVuelo,
    DateTime? ReprogramadoEn,
    DateTime CreadoEn);
