using Domain.Common;

namespace Domain.Entities.Payments;

public sealed class PaymentMethodType : LookupEntity
{
    private PaymentMethodType() { }

    public PaymentMethodType(string nombre) => Nombre = nombre;
}
