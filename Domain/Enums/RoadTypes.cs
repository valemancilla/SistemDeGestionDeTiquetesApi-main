using Domain.Entities.Addresses;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>roadtypes</c>. Entidad: <see cref="RoadType"/>.</summary>
public static class RoadTypes
{
    public const string TableName = "roadtypes";
    public const int NombreMaxLength = 50;

    public const int CalleId = 1;
    public const string CalleNombre = "Calle";

    public const int CarreraId = 2;
    public const string CarreraNombre = "Carrera";

    public const int AvenidaId = 3;
    public const string AvenidaNombre = "Avenida";

    public const int DiagonalId = 4;
    public const string DiagonalNombre = "Diagonal";

    public const int TransversalId = 5;
    public const string TransversalNombre = "Transversal";

    public const int CircunvalarId = 6;
    public const string CircunvalarNombre = "Circunvalar";
}
