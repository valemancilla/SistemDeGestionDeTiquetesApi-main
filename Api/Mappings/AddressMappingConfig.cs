using Api.Dtos.Addresses;
using Domain.Entities.Addresses;
using Mapster;

namespace Api.Mappings;

public sealed class AddressMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Address, AddressDto>()
            .Map(dest => dest.CodigoPostal, src => src.CodigoPostal != null ? src.CodigoPostal.Value : null)
            .Map(dest => dest.NombreTipoVia, src => src.TipoVia != null ? src.TipoVia.Nombre : null)
            .Map(dest => dest.NombreCiudad,  src => src.Ciudad  != null ? src.Ciudad.Nombre  : null);
    }
}
