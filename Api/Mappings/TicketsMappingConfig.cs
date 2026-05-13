using Api.Dtos.Tickets;
using Domain.Entities.Tickets;
using Mapster;

namespace Api.Mappings;

public sealed class TicketsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Ticket, TicketDto>()
            .Map(dest => dest.CodigoTiquete, src => src.CodigoTiquete.Value)
            .Map(dest => dest.NombreEstado, src => src.EstadoTiquete != null ? src.EstadoTiquete.Nombre : null)
            .Map(dest => dest.CreadoEn, src => src.CreatedAt);
    }
}
