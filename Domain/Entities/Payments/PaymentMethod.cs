using Domain.Common;

namespace Domain.Entities.Payments;

public sealed class PaymentMethod : BaseEntity<int>
{
    public int TipoMedioPagoId { get; private set; }
    public int? TipoTarjetaId { get; private set; }
    public int? EmisorTarjetaId { get; private set; }
    public string NombreComercial { get; private set; } = default!;

    public PaymentMethodType? TipoMedioPago { get; private set; }
    public CardType? TipoTarjeta { get; private set; }
    public CardIssuer? EmisorTarjeta { get; private set; }

    private PaymentMethod() { }

    public PaymentMethod(int tipoMedioPagoId, string nombreComercial,
        int? tipoTarjetaId = null, int? emisorTarjetaId = null)
    {
        TipoMedioPagoId = tipoMedioPagoId;
        NombreComercial = nombreComercial;
        TipoTarjetaId = tipoTarjetaId;
        EmisorTarjetaId = emisorTarjetaId;
    }

    public void Update(string nombreComercial, int? tipoTarjetaId, int? emisorTarjetaId)
    {
        NombreComercial = nombreComercial;
        TipoTarjetaId = tipoTarjetaId;
        EmisorTarjetaId = emisorTarjetaId;
    }
}
