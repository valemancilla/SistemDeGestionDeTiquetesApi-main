using Domain.Entities.Routes;
using MediatR;

namespace Application.UseCase.Routes;

public sealed record GetRoutes : IRequest<IReadOnlyList<FlightRoute>>;
