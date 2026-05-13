using Application.Abstractions;
using Domain.Entities.Passengers;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Passengers;

public sealed record GetPassengers : IRequest<IReadOnlyList<Passenger>>;
public sealed class GetPassengersHandler : IRequestHandler<GetPassengers, IReadOnlyList<Passenger>>
{
    private readonly IUnitOfWork _uow;
    public GetPassengersHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<Passenger>> Handle(GetPassengers _, CancellationToken ct)
        => _uow.Repository<Passenger>().GetAllAsync(ct);
}

public sealed record GetPassengerById(int Id) : IRequest<Passenger?>;
public sealed class GetPassengerByIdHandler : IRequestHandler<GetPassengerById, Passenger?>
{
    private readonly IUnitOfWork _uow;
    public GetPassengerByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<Passenger?> Handle(GetPassengerById req, CancellationToken ct)
        => _uow.Repository<Passenger>().GetByIdAsync(req.Id, ct);
}

public sealed record CreatePassenger(int PersonaId, int TipoPasajeroId) : IRequest<int>;
public sealed class CreatePassengerHandler : IRequestHandler<CreatePassenger, int>
{
    private readonly IUnitOfWork _uow;
    public CreatePassengerHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreatePassenger req, CancellationToken ct)
    {
        var item = new Passenger(req.PersonaId, req.TipoPasajeroId);
        await _uow.Repository<Passenger>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreatePassengerValidator : AbstractValidator<CreatePassenger>
{
    public CreatePassengerValidator()
    {
        RuleFor(x => x.PersonaId).GreaterThan(0);
        RuleFor(x => x.TipoPasajeroId).GreaterThan(0);
    }
}

public sealed record UpdatePassenger(int Id, int TipoPasajeroId) : IRequest;
public sealed class UpdatePassengerHandler : IRequestHandler<UpdatePassenger>
{
    private readonly IUnitOfWork _uow;
    public UpdatePassengerHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdatePassenger req, CancellationToken ct)
    {
        var item = await _uow.Repository<Passenger>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Passenger {req.Id} not found.");
        item.Update(req.TipoPasajeroId);
        await _uow.Repository<Passenger>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeletePassenger(int Id) : IRequest;
public sealed class DeletePassengerHandler : IRequestHandler<DeletePassenger>
{
    private readonly IUnitOfWork _uow;
    public DeletePassengerHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeletePassenger req, CancellationToken ct)
    {
        var item = await _uow.Repository<Passenger>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Passenger {req.Id} not found.");
        await _uow.Repository<Passenger>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}