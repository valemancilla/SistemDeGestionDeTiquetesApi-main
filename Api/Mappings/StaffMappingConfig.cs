using Api.Dtos.Staff;
using Domain.Entities.Staff;
using Mapster;

namespace Api.Mappings;

public sealed class StaffMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Staff, StaffDto>()
            .Map(dest => dest.Nombres, src => src.Persona != null ? src.Persona.Nombres : null)
            .Map(dest => dest.Apellidos, src => src.Persona != null ? src.Persona.Apellidos : null)
            .Map(dest => dest.NombreCargo, src => src.Cargo != null ? src.Cargo.Nombre : null)
            .Map(dest => dest.NombreAerolinea, src => src.Aerolinea != null ? src.Aerolinea.Nombre : null)
            .Map(dest => dest.NombreAeropuerto, src => src.Aeropuerto != null ? src.Aeropuerto.Nombre : null);

        config.NewConfig<StaffAvailability, StaffAvailabilityDto>()
            .Map(dest => dest.NombreEstado, src => src.EstadoDisponibilidad != null ? src.EstadoDisponibilidad.Nombre : null);
    }
}
