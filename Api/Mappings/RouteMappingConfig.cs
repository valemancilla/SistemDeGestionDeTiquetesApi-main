using Api.Dtos.Routes;
using Domain.Entities.Routes;
using Mapster;

namespace Api.Mappings;

public sealed class RouteMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FlightRoute, RouteDto>()
            .Map(dest => dest.NombreOrigen, src => src.AeropuertoOrigen != null ? src.AeropuertoOrigen.Nombre : null)
            .Map(dest => dest.CodigoIataOrigen, src => src.AeropuertoOrigen != null ? src.AeropuertoOrigen.CodigoIata.Value : null)
            .Map(dest => dest.NombreDestino, src => src.AeropuertoDestino != null ? src.AeropuertoDestino.Nombre : null)
            .Map(dest => dest.CodigoIataDestino, src => src.AeropuertoDestino != null ? src.AeropuertoDestino.CodigoIata.Value : null);

        config.NewConfig<RouteStop, RouteStopDto>()
            .Map(dest => dest.NombreAeropuertoEscala, src => src.AeropuertoEscala != null ? src.AeropuertoEscala.Nombre : null)
            .Map(dest => dest.CodigoIataEscala, src => src.AeropuertoEscala != null ? src.AeropuertoEscala.CodigoIata.Value : null);
    }
}
