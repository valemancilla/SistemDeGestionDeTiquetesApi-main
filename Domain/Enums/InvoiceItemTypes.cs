using Domain.Entities.Invoices;

namespace Domain.Enums;

/// <summary>Catálogo sembrado en <c>invoiceitemtypes</c>. Entidad: <see cref="InvoiceItemType"/>.</summary>
public static class InvoiceItemTypes
{
    public const string TableName = "invoice_item_types";
    public const int NombreMaxLength = 100;

    public const int TarifaBaseId = 1;
    public const string TarifaBaseNombre = "Tarifa base";

    public const int ImpuestosId = 2;
    public const string ImpuestosNombre = "Impuestos";

    public const int TasasAeroportuariasId = 3;
    public const string TasasAeroportuariasNombre = "Tasas aeroportuarias";

    public const int CargosServicioId = 4;
    public const string CargosServicioNombre = "Cargos por servicio";
}
