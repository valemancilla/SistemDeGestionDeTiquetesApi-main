using Domain.Entities.Reservations;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>reservationstatuses</c>. Entidad: <see cref="ReservationStatus"/>.</summary>
public static class ReservationStatuses
{
    public const string TableName = "reservationstatus";
    public const int NombreMaxLength = 50;

    public const int CreadaId = 1;
    public const string CreadaNombre = "Creada";

    public const int ConfirmadaId = 2;
    public const string ConfirmadaNombre = "Confirmada";

    public const int CanceladaId = 3;
    public const string CanceladaNombre = "Cancelada";

    public const int CompletadaId = 4;
    public const string CompletadaNombre = "Completada";
}
