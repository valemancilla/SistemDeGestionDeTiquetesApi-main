using Domain.Entities.Flights;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>flightstates</c>. Entidad: <see cref="FlightState"/>.</summary>
public static class FlightStates
{
    public const string TableName = "flight_statuses";
    public const int NombreMaxLength = 50;

    public const int ProgramadoId = 1;
    public const string ProgramadoNombre = "Programado";

    public const int EnVueloId = 2;
    public const string EnVueloNombre = "En vuelo";

    public const int AterrizadoId = 3;
    public const string AterrizadoNombre = "Aterrizado";

    public const int CanceladoId = 4;
    public const string CanceladoNombre = "Cancelado";
}
