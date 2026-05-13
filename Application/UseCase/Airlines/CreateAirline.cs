using MediatR;

namespace Application.UseCase.Airlines;

public sealed record CreateAirline(
    string Nombre,
    string CodigoIata,
    int PaisOrigenId
) : IRequest<int>;
