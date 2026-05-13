using Api.Dtos.Aircraft;
using Api.Dtos.Cabin;
using Domain.Entities.Aircraft;
using Mapster;

namespace Api.Mappings;

public sealed class AircraftMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AircraftManufacturer, AircraftManufacturerDto>()
            .Map(dest => dest.NombrePais, src => src.Pais != null ? src.Pais.Nombre : null);

        config.NewConfig<AircraftModel, AircraftModelDto>()
            .Map(dest => dest.NombreFabricante, src => src.Fabricante != null ? src.Fabricante.Nombre : null);

        config.NewConfig<Aircraft, AircraftDto>()
            .Map(dest => dest.Matricula, src => src.Matricula.Value)
            .Map(dest => dest.NombreModelo, src => src.Modelo != null ? src.Modelo.NombreModelo : null)
            .Map(dest => dest.NombreAerolinea, src => src.Aerolinea != null ? src.Aerolinea.Nombre : null);

        config.NewConfig<CabinConfiguration, CabinConfigurationDto>()
            .Map(dest => dest.LetrasAsientos, src => src.LetrasAsientos.Value)
            .Map(dest => dest.NombreAeronave, src => src.Aeronave != null ? src.Aeronave.Matricula.Value : null)
            .Map(dest => dest.NombreTipoCabina, src => src.TipoCabina != null ? src.TipoCabina.Nombre : null);
    }
}
