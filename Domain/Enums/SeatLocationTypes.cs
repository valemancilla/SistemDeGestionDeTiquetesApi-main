using Domain.Entities.Flights;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>seatlocationtypes</c>. Entidad: <see cref="SeatLocationType"/>.</summary>
public static class SeatLocationTypes
{
    public const string TableName = "seat_location_types";
    public const int NombreMaxLength = 50;

    public const int VentanaId = 1;
    public const string VentanaNombre = "Ventana";

    public const int CentroId = 2;
    public const string CentroNombre = "Centro";

    public const int PasilloId = 3;
    public const string PasilloNombre = "Pasillo";
}
