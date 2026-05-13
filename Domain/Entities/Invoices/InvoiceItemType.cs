using Domain.Common;

namespace Domain.Entities.Invoices;

public sealed class InvoiceItemType : LookupEntity
{
    private InvoiceItemType() { }

    public InvoiceItemType(string nombre) => Nombre = nombre;
}
