using Domain.Entities.Staff;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>staffroles</c>. Entidad: <see cref="StaffRole"/>.</summary>
public static class StaffRoles
{
    public const string TableName = "staff_positions";
    public const int NombreMaxLength = 100;

    public const int ComandanteId = 1;
    public const string ComandanteNombre = "Comandante";

    public const int CopilotoId = 2;
    public const string CopilotoNombre = "Copiloto";

    public const int IngenieroVueloId = 3;
    public const string IngenieroVueloNombre = "Ingeniero de vuelo";

    public const int SobrecargoId = 4;
    public const string SobrecargoNombre = "Sobrecargo";

    public const int AuxiliarCabinaId = 5;
    public const string AuxiliarCabinaNombre = "Auxiliar de cabina";
}
