using Api.Dtos.Flights;
using Domain.Entities.Flights;
using Mapster;

namespace Api.Mappings;

public sealed class FlightMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Flight, FlightDto>()
            .Map(dest => dest.CodigoVuelo, src => src.CodigoVuelo.Value)
            .Map(dest => dest.NombreAerolinea, src => src.Aerolinea != null ? src.Aerolinea.Nombre : null)
            .Map(dest => dest.EstadoVuelo, src => src.EstadoVuelo != null ? src.EstadoVuelo.Nombre : null)
            .Map(dest => dest.CreadoEn, src => src.CreatedAt);

        config.NewConfig<FlightHistory, FlightHistoryDto>()
            .Map(dest => dest.NombreEstadoAnterior, src => src.EstadoAnterior != null ? src.EstadoAnterior.Nombre : null)
            .Map(dest => dest.NombreEstadoNuevo, src => src.EstadoNuevo != null ? src.EstadoNuevo.Nombre : null);

        config.NewConfig<FlightStatusTransition, FlightStatusTransitionDto>()
            .Map(dest => dest.NombreEstadoOrigen, src => src.EstadoOrigen != null ? src.EstadoOrigen.Nombre : null)
            .Map(dest => dest.NombreEstadoDestino, src => src.EstadoDestino != null ? src.EstadoDestino.Nombre : null);

        config.NewConfig<FlightAssignment, FlightAssignmentDto>()
            .Map(dest => dest.NombrePersonal, src => src.Personal != null
                ? $"{src.Personal.Persona!.Nombres} {src.Personal.Persona.Apellidos}" : null)
            .Map(dest => dest.NombreRolVuelo, src => src.RolVuelo != null ? src.RolVuelo.Nombre : null);

        config.NewConfig<FlightSeat, FlightSeatDto>()
            .Map(dest => dest.CodigoAsiento, src => src.CodigoAsiento.Value)
            .Map(dest => dest.NombreTipoCabina, src => src.TipoCabina != null ? src.TipoCabina.Nombre : null)
            .Map(dest => dest.NombreTipoUbicacion, src => src.TipoUbicacion != null ? src.TipoUbicacion.Nombre : null);
    }
}
