using Domain.Common;

namespace Domain.Entities.Payments;

public sealed class PaymentState : LookupEntity
{
    private PaymentState() { }

    public PaymentState(string nombre) => Nombre = nombre;
}
