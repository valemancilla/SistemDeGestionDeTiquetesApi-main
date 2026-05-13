using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Common;

public sealed record CreateLookupRequest([Required][MaxLength(100)] string Nombre);
