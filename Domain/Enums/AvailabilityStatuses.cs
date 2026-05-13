using Domain.Entities.Staff;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>availabilitystatuses</c>. Entidad: <see cref="AvailabilityStatus"/>.</summary>
public static class AvailabilityStatuses
{
    public const string TableName = "availability_statuses";
    public const int NombreMaxLength = 50;

    public const int DisponibleId = 1;
    public const string DisponibleNombre = "Disponible";

    public const int NoDisponibleId = 2;
    public const string NoDisponibleNombre = "No disponible";

    public const int EnLicenciaId = 3;
    public const string EnLicenciaNombre = "En licencia";
}
