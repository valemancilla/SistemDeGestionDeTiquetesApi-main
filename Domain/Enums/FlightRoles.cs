using Domain.Entities.Flights;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>flightroles</c>. Entidad: <see cref="FlightRole"/>.</summary>
public static class FlightRoles
{
    public const string TableName = "flight_roles";
    public const int NombreMaxLength = 100;

    public const int PilotoComandanteId = 1;
    public const string PilotoComandanteNombre = "Piloto comandante";

    public const int PilotoCopilotoId = 2;
    public const string PilotoCopilotoNombre = "Piloto copiloto";

    public const int JefeCabinaId = 3;
    public const string JefeCabinaNombre = "Jefe de cabina";

    public const int TripulanteCabinaId = 4;
    public const string TripulanteCabinaNombre = "Tripulante de cabina";
}
