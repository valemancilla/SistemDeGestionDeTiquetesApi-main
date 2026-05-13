using Application.Abstractions;
using Domain.Entities.Flights;
using MediatR;

namespace Application.UseCase.Flights;

public sealed class GetFlightsHandler : IRequestHandler<GetFlights, IReadOnlyList<Flight>>
{
    private readonly IUnitOfWork _uow;

    public GetFlightsHandler(IUnitOfWork uow) => _uow = uow;

    public Task<IReadOnlyList<Flight>> Handle(GetFlights request, CancellationToken ct)
        => _uow.Flights.GetAllAsync(ct);
}
