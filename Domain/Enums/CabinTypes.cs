using Domain.Entities.Aircraft;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>cabintypes</c>. Entidad: <see cref="CabinType"/>.</summary>
public static class CabinTypes
{
    public const string TableName = "cabintypes";
    public const int NombreMaxLength = 50;

    public const int EconomicaId = 1;
    public const string EconomicaNombre = "Económica";

    public const int PremiumEconomyId = 2;
    public const string PremiumEconomyNombre = "Premium economy";

    public const int BusinessId = 3;
    public const string BusinessNombre = "Business";

    public const int PrimeraId = 4;
    public const string PrimeraNombre = "Primera";
}
