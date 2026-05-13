using Domain.Entities.Flights;
using MediatR;

namespace Application.UseCase.Flights;

public sealed record GetFlights : IRequest<IReadOnlyList<Flight>>;
