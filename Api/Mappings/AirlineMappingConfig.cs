using Api.Dtos.Airlines;
using Domain.Entities.Airlines;
using Mapster;

namespace Api.Mappings;

public sealed class AirlineMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Airline, AirlineDto>()
            .Map(dest => dest.CodigoIata, src => src.CodigoIata.Value)
            .Map(dest => dest.CreadoEn, src => src.CreatedAt)
            .Map(dest => dest.NombrePais, src => src.PaisOrigen != null ? src.PaisOrigen.Nombre : null);
    }
}
