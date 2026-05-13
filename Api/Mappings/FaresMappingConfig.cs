using Api.Dtos.Fares;
using Domain.Entities.Fares;
using Mapster;

namespace Api.Mappings;

public sealed class FaresMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Season, SeasonDto>();

        config.NewConfig<PassengerType, PassengerTypeDto>();

        config.NewConfig<Fare, FareDto>()
            .Map(dest => dest.NombreTipoCabina, src => src.TipoCabina != null ? src.TipoCabina.Nombre : null)
            .Map(dest => dest.NombreTipoPasajero, src => src.TipoPasajero != null ? src.TipoPasajero.Nombre : null)
            .Map(dest => dest.NombreTemporada, src => src.Temporada != null ? src.Temporada.Nombre : null);
    }
}
