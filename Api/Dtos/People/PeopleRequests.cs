using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.People;

public sealed record CreateDocumentTypeRequest(
    [Required][StringLength(255, MinimumLength = 1)] string Nombre,
    [Required][StringLength(255, MinimumLength = 1)][RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Código: 1 a 255 letras o números.")]
    string Codigo);

public sealed record UpdateDocumentTypeRequest(
    [Required][StringLength(255, MinimumLength = 1)] string Nombre,
    [Required][StringLength(255, MinimumLength = 1)][RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Código: 1 a 255 letras o números.")]
    string Codigo);

public sealed record CreatePersonRequest(
    [Range(1, int.MaxValue)] int TipoDocumentoId,
    [Required][StringLength(30, MinimumLength = 1)][RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "Documento: letras, números o guion.")]
    string NumeroDocumento,
    [Required][StringLength(100, MinimumLength = 1)] string Nombres,
    [Required][StringLength(100, MinimumLength = 1)] string Apellidos,
    DateOnly? FechaNacimiento,
    [MaxLength(1)] string? Genero,
    int? DireccionId);

public sealed record UpdatePersonRequest(
    [Required][StringLength(100, MinimumLength = 1)] string Nombres,
    [Required][StringLength(100, MinimumLength = 1)] string Apellidos,
    [MaxLength(1)] string? Genero,
    int? DireccionId);

public sealed record CreateEmailDomainRequest(
    [Required][StringLength(100, MinimumLength = 4)][RegularExpression(@"^(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,63}$", ErrorMessage = "Dominio DNS no válido (ej. correo.com).")]
    string Dominio);

public sealed record CreatePhoneCodeRequest(
    [Required][StringLength(5, MinimumLength = 1)][RegularExpression(@"^\+?[0-9]{1,4}$", ErrorMessage = "Indicativo: + opcional y hasta 4 dígitos (máx. 5 caracteres).")]
    string CodigoPais,
    [Required][MaxLength(100)] string NombrePais);

public sealed record UpdatePhoneCodeRequest(
    [Required][StringLength(5, MinimumLength = 1)][RegularExpression(@"^\+?[0-9]{1,4}$", ErrorMessage = "Indicativo: + opcional y hasta 4 dígitos (máx. 5 caracteres).")]
    string CodigoPais,
    [Required][MaxLength(100)] string NombrePais);

public sealed record CreatePersonEmailRequest(
    [Range(1, int.MaxValue)] int PersonaId,
    [Required][StringLength(100, MinimumLength = 1)][RegularExpression(@"^[a-zA-Z0-9](?:[a-zA-Z0-9._+-]{0,98}[a-zA-Z0-9])?$", ErrorMessage = "Parte local del correo no válida.")]
    string UsuarioEmail,
    [Range(1, int.MaxValue)] int DominioEmailId,
    bool EsPrincipal);

public sealed record UpdatePersonEmailRequest(
    [Required][StringLength(100, MinimumLength = 1)][RegularExpression(@"^[a-zA-Z0-9](?:[a-zA-Z0-9._+-]{0,98}[a-zA-Z0-9])?$", ErrorMessage = "Parte local del correo no válida.")]
    string UsuarioEmail,
    [Range(1, int.MaxValue)] int DominioEmailId,
    bool EsPrincipal);

public sealed record CreatePersonPhoneRequest(
    [Range(1, int.MaxValue)] int PersonaId,
    [Range(1, int.MaxValue)] int CodigoTelefonoId,
    [Required][StringLength(20, MinimumLength = 1)][RegularExpression(@"^[0-9 \-]+$", ErrorMessage = "Solo dígitos, espacios o guiones (5 a 15 dígitos en total).")]
    string NumeroTelefono,
    bool EsPrincipal);

public sealed record UpdatePersonPhoneRequest(
    [Range(1, int.MaxValue)] int CodigoTelefonoId,
    [Required][StringLength(20, MinimumLength = 1)][RegularExpression(@"^[0-9 \-]+$", ErrorMessage = "Solo dígitos, espacios o guiones (5 a 15 dígitos en total).")]
    string NumeroTelefono,
    bool EsPrincipal);
