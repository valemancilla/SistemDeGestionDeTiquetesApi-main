using Domain.Entities.Payments;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>paymentmethodtypes</c>. Entidad: <see cref="PaymentMethodType"/>.</summary>
public static class PaymentMethodTypes
{
    public const string TableName = "payment_method_types";
    public const int NombreMaxLength = 50;

    public const int TarjetaCreditoId = 1;
    public const string TarjetaCreditoNombre = "Tarjeta de crédito";

    public const int TarjetaDebitoId = 2;
    public const string TarjetaDebitoNombre = "Tarjeta de débito";

    public const int EfectivoId = 3;
    public const string EfectivoNombre = "Efectivo";

    public const int TransferenciaId = 4;
    public const string TransferenciaNombre = "Transferencia";
}
