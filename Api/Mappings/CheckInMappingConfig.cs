using Api.Dtos.CheckIn;
using Domain.Entities.CheckIn;
using Mapster;

namespace Api.Mappings;

public sealed class CheckInMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CheckIn, CheckInDto>()
            .Map(dest => dest.CodigoAsiento, src => src.AsientoVuelo != null ? src.AsientoVuelo.CodigoAsiento.Value : null)
            .Map(dest => dest.NumeroTarjetaEmbarque, src => src.NumeroTarjetaEmbarque.Value)
            .Map(dest => dest.NombreEstado, src => src.EstadoCheckin != null ? src.EstadoCheckin.Nombre : null);
    }
}
