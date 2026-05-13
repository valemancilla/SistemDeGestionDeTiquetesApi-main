using Domain.Entities.Flights;
using MediatR;

namespace Application.UseCase.Flights;

public sealed record GetFlightById(int Id) : IRequest<Flight?>;
