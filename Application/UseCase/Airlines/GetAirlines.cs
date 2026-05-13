using Domain.Entities.Airlines;
using MediatR;

namespace Application.UseCase.Airlines;

public sealed record GetAirlines(bool SoloActivas = false) : IRequest<IReadOnlyList<Airline>>;
