using Domain.Entities.Payments;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>cardissuers</c>. Entidad: <see cref="CardIssuer"/>.</summary>
public static class CardIssuers
{
    public const string TableName = "card_issuers";
    public const int NombreMaxLength = 50;

    public const int VisaId = 1;
    public const string VisaNombre = "Visa";

    public const int MasterCardId = 2;
    public const string MasterCardNombre = "Mastercard";

    public const int AmericanExpressId = 3;
    public const string AmericanExpressNombre = "American Express";
}
