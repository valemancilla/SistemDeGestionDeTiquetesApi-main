using Api.Dtos.Passengers;
using Domain.Entities.Passengers;
using Mapster;

namespace Api.Mappings;

public sealed class PassengersMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Passenger, PassengerDto>()
            .Map(dest => dest.Nombres, src => src.Persona != null ? src.Persona.Nombres : null)
            .Map(dest => dest.Apellidos, src => src.Persona != null ? src.Persona.Apellidos : null)
            .Map(dest => dest.NombreTipoPasajero, src => src.TipoPasajero != null ? src.TipoPasajero.Nombre : null);
    }
}
