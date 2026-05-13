namespace Api.Dtos.Airlines;

public sealed record AirlineDto(
    int Id,
    string Nombre,
    string CodigoIata,
    int PaisOrigenId,
    string? NombrePais,
    bool Activa,
    DateTime CreadoEn);
