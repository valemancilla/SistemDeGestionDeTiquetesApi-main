using Api.Dtos.Airport;
using Domain.Entities.Airports;
using Mapster;

namespace Api.Mappings;

public sealed class AirportMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Airport, AirportDto>()
            .Map(dest => dest.CodigoIata, src => src.CodigoIata.Value)
            .Map(dest => dest.CodigoIcao, src => src.CodigoIcao != null ? src.CodigoIcao.Value : null)
            .Map(dest => dest.NombreCiudad, src => src.Ciudad != null ? src.Ciudad.Nombre : null);

        config.NewConfig<AirportAirline, AirportAirlineDto>()
            .Map(dest => dest.Terminal, src => src.Terminal != null ? src.Terminal.Value : null)
            .Map(dest => dest.NombreAeropuerto, src => src.Aeropuerto != null ? src.Aeropuerto.Nombre : null)
            .Map(dest => dest.NombreAerolinea, src => src.Aerolinea != null ? src.Aerolinea.Nombre : null);
    }
}
