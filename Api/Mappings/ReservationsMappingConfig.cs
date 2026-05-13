using Api.Dtos.Reservations;
using Domain.Entities.Reservations;
using Mapster;

namespace Api.Mappings;

public sealed class ReservationsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Reservation, ReservationDto>()
            .Map(dest => dest.CodigoReserva, src => src.CodigoReserva.Value)
            .Map(dest => dest.NombreEstado, src => src.EstadoReserva != null ? src.EstadoReserva.Nombre : null)
            .Map(dest => dest.FechaReserva, src => src.CreatedAt);

        config.NewConfig<ReservationFlight, ReservationFlightDto>()
            .Map(dest => dest.CodigoVuelo, src => src.Vuelo != null ? src.Vuelo.CodigoVuelo.Value : null);

        config.NewConfig<ReservationPassenger, ReservationPassengerDto>()
            .Map(dest => dest.NombresPasajero, src => src.Pasajero != null && src.Pasajero.Persona != null ? src.Pasajero.Persona.Nombres : null)
            .Map(dest => dest.ApellidosPasajero, src => src.Pasajero != null && src.Pasajero.Persona != null ? src.Pasajero.Persona.Apellidos : null);

        config.NewConfig<ReservationStatusTransition, ReservationStatusTransitionDto>()
            .Map(dest => dest.NombreEstadoOrigen, src => src.EstadoOrigen != null ? src.EstadoOrigen.Nombre : null)
            .Map(dest => dest.NombreEstadoDestino, src => src.EstadoDestino != null ? src.EstadoDestino.Nombre : null);
    }
}
