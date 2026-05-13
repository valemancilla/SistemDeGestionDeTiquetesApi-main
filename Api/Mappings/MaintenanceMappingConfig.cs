using Api.Dtos.Maintenance;
using Domain.Entities.Maintenance;
using Mapster;

namespace Api.Mappings;

public sealed class MaintenanceMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MaintenanceType, MaintenanceTypeDto>();

        config.NewConfig<AircraftMaintenance, AircraftMaintenanceDto>()
            .Map(dest => dest.MatriculaAeronave, src => src.Aeronave != null ? src.Aeronave.Matricula.Value : null)
            .Map(dest => dest.NombreTipoMantenimiento, src => src.TipoMantenimiento != null ? src.TipoMantenimiento.Nombre : null);
    }
}
