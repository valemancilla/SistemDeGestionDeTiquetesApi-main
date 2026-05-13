using Application.Abstractions;
using Domain.Entities.Reservations;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Reservations;

public sealed record GetReservationStatusTransitions : IRequest<IReadOnlyList<ReservationStatusTransition>>;
public sealed class GetReservationStatusTransitionsHandler : IRequestHandler<GetReservationStatusTransitions, IReadOnlyList<ReservationStatusTransition>>
{
    private readonly IUnitOfWork _uow;
    public GetReservationStatusTransitionsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<ReservationStatusTransition>> Handle(GetReservationStatusTransitions _, CancellationToken ct)
        => _uow.Repository<ReservationStatusTransition>().GetAllAsync(ct);
}

public sealed record GetReservationStatusTransitionById(int Id) : IRequest<ReservationStatusTransition?>;
public sealed class GetReservationStatusTransitionByIdHandler : IRequestHandler<GetReservationStatusTransitionById, ReservationStatusTransition?>
{
    private readonly IUnitOfWork _uow;
    public GetReservationStatusTransitionByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<ReservationStatusTransition?> Handle(GetReservationStatusTransitionById req, CancellationToken ct)
        => _uow.Repository<ReservationStatusTransition>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateReservationStatusTransition(int EstadoOrigenId, int EstadoDestinoId) : IRequest<int>;
public sealed class CreateReservationStatusTransitionHandler : IRequestHandler<CreateReservationStatusTransition, int>
{
    private readonly IUnitOfWork _uow;
    public CreateReservationStatusTransitionHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateReservationStatusTransition req, CancellationToken ct)
    {
        var item = new ReservationStatusTransition(req.EstadoOrigenId, req.EstadoDestinoId);
        await _uow.Repository<ReservationStatusTransition>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateReservationStatusTransitionValidator : AbstractValidator<CreateReservationStatusTransition>
{
    public CreateReservationStatusTransitionValidator()
    {
        RuleFor(x => x.EstadoOrigenId).GreaterThan(0);
        RuleFor(x => x.EstadoDestinoId).GreaterThan(0);
    }
}

public sealed record DeleteReservationStatusTransition(int Id) : IRequest;
public sealed class DeleteReservationStatusTransitionHandler : IRequestHandler<DeleteReservationStatusTransition>
{
    private readonly IUnitOfWork _uow;
    public DeleteReservationStatusTransitionHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteReservationStatusTransition req, CancellationToken ct)
    {
        var item = await _uow.Repository<ReservationStatusTransition>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"ReservationStatusTransition {req.Id} not found.");
        await _uow.Repository<ReservationStatusTransition>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}