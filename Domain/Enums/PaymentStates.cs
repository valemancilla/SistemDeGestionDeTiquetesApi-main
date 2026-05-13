using Domain.Common;
using Domain.Entities.Payments;

namespace Domain.Enums;

/// <summary>
/// Alineación con la tabla <c>paymentstates</c> (PostgreSQL). El modelo de dominio y EF es <see cref="PaymentState"/>.
/// Los estados son filas con <c>id</c> y <c>nombre</c>; la API usa <c>EstadoPagoId</c> en <see cref="Payment"/>.
/// </summary>
public static class PaymentStates
{
    /// <summary>Nombre físico de la tabla en base de datos.</summary>
    public const string TableName = "payment_statuses";

    /// <summary>Límite de <see cref="LookupEntity.Nombre"/> en columna <c>nombre</c>.</summary>
    public const int NombreMaxLength = 50;

    public const int PendienteId = 1;
    public const string PendienteNombre = "Pendiente";

    public const int PagadoId = 2;
    public const string PagadoNombre = "Pagado";

    public const int RechazadoId = 3;
    public const string RechazadoNombre = "Rechazado";

    public const int ReembolsadoId = 4;
    public const string ReembolsadoNombre = "Reembolsado";
}
