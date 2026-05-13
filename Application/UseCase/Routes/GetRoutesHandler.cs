using Application.Abstractions;
using Domain.Entities.Routes;
using MediatR;

namespace Application.UseCase.Routes;

public sealed class GetRoutesHandler : IRequestHandler<GetRoutes, IReadOnlyList<FlightRoute>>
{
    private readonly IUnitOfWork _uow;

    public GetRoutesHandler(IUnitOfWork uow) => _uow = uow;

    public Task<IReadOnlyList<FlightRoute>> Handle(GetRoutes request, CancellationToken ct)
        => _uow.Routes.GetAllAsync(ct);
}
