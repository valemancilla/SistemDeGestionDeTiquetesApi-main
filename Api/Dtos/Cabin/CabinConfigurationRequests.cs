using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Cabin;

public sealed record CreateCabinConfigurationRequest(
    [Range(1, int.MaxValue)] int AeronaveId,
    [Range(1, int.MaxValue)] int TipoCabinaId,
    [Range(1, int.MaxValue)] int FilaInicio,
    [Range(1, int.MaxValue)] int FilaFin,
    [Range(1, 20)] int AsientosPorFila,
    [Required][StringLength(10, MinimumLength = 1)][RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Letras de fila: solo A-Z, de 1 a 10 caracteres.")]
    string LetrasAsientos);

public sealed record UpdateCabinConfigurationRequest(
    [Range(1, int.MaxValue)] int FilaInicio,
    [Range(1, int.MaxValue)] int FilaFin,
    [Range(1, 20)] int AsientosPorFila,
    [Required][StringLength(10, MinimumLength = 1)][RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Letras de fila: solo A-Z, de 1 a 10 caracteres.")]
    string LetrasAsientos);
