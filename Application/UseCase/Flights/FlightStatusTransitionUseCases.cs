using Application.Abstractions;
using Domain.Entities.Flights;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Flights;

public sealed record GetFlightStatusTransitions : IRequest<IReadOnlyList<FlightStatusTransition>>;
public sealed class GetFlightStatusTransitionsHandler : IRequestHandler<GetFlightStatusTransitions, IReadOnlyList<FlightStatusTransition>>
{
    private readonly IUnitOfWork _uow;
    public GetFlightStatusTransitionsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<FlightStatusTransition>> Handle(GetFlightStatusTransitions _, CancellationToken ct)
        => _uow.Repository<FlightStatusTransition>().GetAllAsync(ct);
}

public sealed record GetFlightStatusTransitionById(int Id) : IRequest<FlightStatusTransition?>;
public sealed class GetFlightStatusTransitionByIdHandler : IRequestHandler<GetFlightStatusTransitionById, FlightStatusTransition?>
{
    private readonly IUnitOfWork _uow;
    public GetFlightStatusTransitionByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<FlightStatusTransition?> Handle(GetFlightStatusTransitionById req, CancellationToken ct)
        => _uow.Repository<FlightStatusTransition>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateFlightStatusTransition(int EstadoOrigenId, int EstadoDestinoId) : IRequest<int>;
public sealed class CreateFlightStatusTransitionHandler : IRequestHandler<CreateFlightStatusTransition, int>
{
    private readonly IUnitOfWork _uow;
    public CreateFlightStatusTransitionHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateFlightStatusTransition req, CancellationToken ct)
    {
        var item = new FlightStatusTransition(req.EstadoOrigenId, req.EstadoDestinoId);
        await _uow.Repository<FlightStatusTransition>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateFlightStatusTransitionValidator : AbstractValidator<CreateFlightStatusTransition>
{
    public CreateFlightStatusTransitionValidator()
    {
        RuleFor(x => x.EstadoOrigenId).GreaterThan(0);
        RuleFor(x => x.EstadoDestinoId).GreaterThan(0);
    }
}

public sealed record DeleteFlightStatusTransition(int Id) : IRequest;
public sealed class DeleteFlightStatusTransitionHandler : IRequestHandler<DeleteFlightStatusTransition>
{
    private readonly IUnitOfWork _uow;
    public DeleteFlightStatusTransitionHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteFlightStatusTransition req, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightStatusTransition>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"FlightStatusTransition {req.Id} not found.");
        await _uow.Repository<FlightStatusTransition>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}