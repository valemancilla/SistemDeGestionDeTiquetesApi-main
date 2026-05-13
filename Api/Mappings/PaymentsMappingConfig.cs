using Api.Dtos.Payments;
using Domain.Entities.Payments;
using Mapster;

namespace Api.Mappings;

public sealed class PaymentsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PaymentMethod, PaymentMethodDto>()
            .Map(d => d.NombreTipoMedio, s => s.TipoMedioPago != null ? s.TipoMedioPago.Nombre : null)
            .Map(d => d.NombreTipoTarjeta, s => s.TipoTarjeta != null ? s.TipoTarjeta.Nombre : null)
            .Map(d => d.NombreEmisor, s => s.EmisorTarjeta != null ? s.EmisorTarjeta.Nombre : null);

        config.NewConfig<Payment, PaymentDto>()
            .Map(d => d.NombreEstado, s => s.EstadoPago != null ? s.EstadoPago.Nombre : null)
            .Map(d => d.NombreMetodoPago, s => s.MetodoPago != null ? s.MetodoPago.NombreComercial : null);
    }
}
