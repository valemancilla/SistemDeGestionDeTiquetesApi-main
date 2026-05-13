using Application.Abstractions;
using Domain.Entities.Routes;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Routes;

public sealed record GetRouteStops : IRequest<IReadOnlyList<RouteStop>>;
public sealed class GetRouteStopsHandler : IRequestHandler<GetRouteStops, IReadOnlyList<RouteStop>>
{
    private readonly IUnitOfWork _uow;
    public GetRouteStopsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<RouteStop>> Handle(GetRouteStops _, CancellationToken ct)
        => _uow.Repository<RouteStop>().GetAllAsync(ct);
}

public sealed record GetRouteStopById(int Id) : IRequest<RouteStop?>;
public sealed class GetRouteStopByIdHandler : IRequestHandler<GetRouteStopById, RouteStop?>
{
    private readonly IUnitOfWork _uow;
    public GetRouteStopByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<RouteStop?> Handle(GetRouteStopById req, CancellationToken ct)
        => _uow.Repository<RouteStop>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateRouteStop(int RutaId, int AeropuertoEscalaId, int Orden, int DuracionEscalaMin = 0) : IRequest<int>;
public sealed class CreateRouteStopHandler : IRequestHandler<CreateRouteStop, int>
{
    private readonly IUnitOfWork _uow;
    public CreateRouteStopHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateRouteStop req, CancellationToken ct)
    {
        var item = new RouteStop(req.RutaId, req.AeropuertoEscalaId, req.Orden, req.DuracionEscalaMin);
        await _uow.Repository<RouteStop>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateRouteStopValidator : AbstractValidator<CreateRouteStop>
{
    public CreateRouteStopValidator()
    {
        RuleFor(x => x.RutaId).GreaterThan(0);
        RuleFor(x => x.AeropuertoEscalaId).GreaterThan(0);
        RuleFor(x => x.Orden).GreaterThan(0);
        RuleFor(x => x.DuracionEscalaMin).GreaterThanOrEqualTo(0);
    }
}

public sealed record UpdateRouteStop(int Id, int Orden, int DuracionEscalaMin) : IRequest;
public sealed class UpdateRouteStopHandler : IRequestHandler<UpdateRouteStop>
{
    private readonly IUnitOfWork _uow;
    public UpdateRouteStopHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateRouteStop req, CancellationToken ct)
    {
        var item = await _uow.Repository<RouteStop>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"RouteStop {req.Id} not found.");
        item.Update(req.Orden, req.DuracionEscalaMin);
        await _uow.Repository<RouteStop>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteRouteStop(int Id) : IRequest;
public sealed class DeleteRouteStopHandler : IRequestHandler<DeleteRouteStop>
{
    private readonly IUnitOfWork _uow;
    public DeleteRouteStopHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteRouteStop req, CancellationToken ct)
    {
        var item = await _uow.Repository<RouteStop>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"RouteStop {req.Id} not found.");
        await _uow.Repository<RouteStop>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
