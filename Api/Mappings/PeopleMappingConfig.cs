using Api.Dtos.People;
using Domain.Entities.People;
using Mapster;

namespace Api.Mappings;

public sealed class PeopleMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<DocumentType, DocumentTypeDto>()
            .Map(dest => dest.Codigo, src => src.Codigo.Value);

        config.NewConfig<Person, PersonDto>()
            .Map(dest => dest.NumeroDocumento, src => src.NumeroDocumento.Value)
            .Map(dest => dest.Nombres, src => src.Nombres.Value)
            .Map(dest => dest.Apellidos, src => src.Apellidos.Value)
            .Map(dest => dest.NombreTipoDocumento, src => src.TipoDocumento != null ? src.TipoDocumento.Nombre : null)
            .Map(dest => dest.Genero, src => src.Genero.HasValue ? src.Genero.Value.ToString() : null);

        config.NewConfig<EmailDomain, EmailDomainDto>()
            .Map(dest => dest.Dominio, src => src.Dominio.Value);

        config.NewConfig<PhoneCode, PhoneCodeDto>()
            .Map(dest => dest.CodigoPais, src => src.CodigoPais.Value);

        config.NewConfig<PersonEmail, PersonEmailDto>()
            .Map(dest => dest.UsuarioEmail, src => src.UsuarioEmail.Value)
            .Map(dest => dest.Dominio, src => src.DominioEmail != null ? src.DominioEmail.Dominio.Value : null);

        config.NewConfig<PersonPhone, PersonPhoneDto>()
            .Map(dest => dest.NumeroTelefono, src => src.NumeroTelefono.Value)
            .Map(dest => dest.CodigoPais, src => src.CodigoTelefono != null ? src.CodigoTelefono.CodigoPais.Value : null);
    }
}
