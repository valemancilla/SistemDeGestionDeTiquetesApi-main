using Domain.Entities.Payments;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>cardtypes</c>. Entidad: <see cref="CardType"/>.</summary>
public static class CardTypes
{
    public const string TableName = "card_types";
    public const int NombreMaxLength = 50;

    public const int CreditoId = 1;
    public const string CreditoNombre = "Crédito";

    public const int DebitoId = 2;
    public const string DebitoNombre = "Débito";

    public const int PrepagoId = 3;
    public const string PrepagoNombre = "Prepago";
}
