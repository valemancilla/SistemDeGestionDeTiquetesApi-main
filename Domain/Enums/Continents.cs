using Domain.Entities.Geography;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>continents</c>. Entidad: <see cref="Continent"/>.</summary>
public static class Continents
{
    public const string TableName = "continents";
    public const int NombreMaxLength = 50;

    public const int AmericaId = 1;
    public const string AmericaNombre = "América";

    public const int EuropaId = 2;
    public const string EuropaNombre = "Europa";

    public const int AsiaId = 3;
    public const string AsiaNombre = "Asia";

    public const int AfricaId = 4;
    public const string AfricaNombre = "África";

    public const int OceaniaId = 5;
    public const string OceaniaNombre = "Oceanía";

    public const int AntartidaId = 6;
    public const string AntartidaNombre = "Antártida";
}
