using Application.Abstractions;
using Domain.Entities.Flights;
using MediatR;

namespace Application.UseCase.Flights;

public sealed class GetFlightByIdHandler : IRequestHandler<GetFlightById, Flight?>
{
    private readonly IUnitOfWork _uow;

    public GetFlightByIdHandler(IUnitOfWork uow) => _uow = uow;

    public Task<Flight?> Handle(GetFlightById request, CancellationToken ct)
        => _uow.Flights.GetByIdAsync(request.Id, ct);
}
