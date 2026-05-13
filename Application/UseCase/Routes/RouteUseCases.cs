using Application.Abstractions;
using Domain.Entities.Routes;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Routes;

public sealed record GetRouteById(int Id) : IRequest<FlightRoute?>;
public sealed class GetRouteByIdHandler : IRequestHandler<GetRouteById, FlightRoute?>
{
    private readonly IUnitOfWork _uow;
    public GetRouteByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<FlightRoute?> Handle(GetRouteById req, CancellationToken ct)
        => _uow.Routes.GetByIdAsync(req.Id, ct);
}

public sealed record UpdateRoute(int Id, int? DistanciaKm, int? DuracionEstimadaMin) : IRequest;
public sealed class UpdateRouteHandler : IRequestHandler<UpdateRoute>
{
    private readonly IUnitOfWork _uow;
    public UpdateRouteHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateRoute req, CancellationToken ct)
    {
        var item = await _uow.Routes.GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"FlightRoute {req.Id} not found.");
        item.Update(req.DistanciaKm, req.DuracionEstimadaMin);
        await _uow.Repository<FlightRoute>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteRoute(int Id) : IRequest;
public sealed class DeleteRouteHandler : IRequestHandler<DeleteRoute>
{
    private readonly IUnitOfWork _uow;
    public DeleteRouteHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteRoute req, CancellationToken ct)
    {
        var item = await _uow.Routes.GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"FlightRoute {req.Id} not found.");
        await _uow.Repository<FlightRoute>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
