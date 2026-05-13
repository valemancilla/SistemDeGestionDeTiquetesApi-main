using Api.Dtos.Invoices;
using Domain.Entities.Invoices;
using Mapster;

namespace Api.Mappings;

public sealed class InvoicesMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Invoice, InvoiceDto>()
            .Map(dest => dest.NumeroFactura, src => src.NumeroFactura.Value)
            .Map(dest => dest.FechaEmision, src => src.CreatedAt);

        config.NewConfig<InvoiceItem, InvoiceItemDto>()
            .Map(dest => dest.NombreTipoItem, src => src.TipoItem != null ? src.TipoItem.Nombre : null);
    }
}
