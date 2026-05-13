using Api.Dtos.Clients;
using Domain.Entities.Clients;
using Mapster;

namespace Api.Mappings;

public sealed class ClientsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Client, ClientDto>()
            .Map(dest => dest.Nombres, src => src.Persona != null ? src.Persona.Nombres : null)
            .Map(dest => dest.Apellidos, src => src.Persona != null ? src.Persona.Apellidos : null);
    }
}
