using Domain.Common;

namespace Domain.Entities.Payments;

public sealed class CardIssuer : LookupEntity
{
    private CardIssuer() { }

    public CardIssuer(string nombre) => Nombre = nombre;
}
