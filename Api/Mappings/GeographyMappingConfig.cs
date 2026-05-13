using Api.Dtos.Geography;
using Domain.Entities.Geography;
using Mapster;

namespace Api.Mappings;

public sealed class GeographyMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Country, CountryDto>()
            .Map(dest => dest.CodigoIso, src => src.CodigoIso.Value)
            .Map(dest => dest.NombreContinente, src => src.Continente != null ? src.Continente.Nombre : null);

        config.NewConfig<Region, RegionDto>()
            .Map(dest => dest.NombrePais, src => src.Pais != null ? src.Pais.Nombre : null);

        config.NewConfig<City, CityDto>()
            .Map(dest => dest.NombreRegion, src => src.Region != null ? src.Region.Nombre : null);
    }
}
