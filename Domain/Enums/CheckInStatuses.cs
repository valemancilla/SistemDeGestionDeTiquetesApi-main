using Domain.Entities.CheckIn;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>checkinstatuses</c>. Entidad: <see cref="CheckInStatus"/>.</summary>
public static class CheckInStatuses
{
    public const string TableName = "checkin_statuses";
    public const int NombreMaxLength = 50;

    public const int PendienteId = 1;
    public const string PendienteNombre = "Pendiente";

    public const int CompletadoId = 2;
    public const string CompletadoNombre = "Completado";

    public const int NoShowId = 3;
    public const string NoShowNombre = "No show";
}
