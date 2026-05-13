using Domain.Common;

namespace Domain.Entities.Payments;

public sealed class CardType : LookupEntity
{
    private CardType() { }

    public CardType(string nombre) => Nombre = nombre;
}
