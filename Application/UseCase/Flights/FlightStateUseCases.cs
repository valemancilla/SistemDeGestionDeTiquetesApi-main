using Application.Abstractions;
using Domain.Entities.Flights;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Flights;

public sealed record GetFlightStates : IRequest<IReadOnlyList<FlightState>>;
public sealed class GetFlightStatesHandler : IRequestHandler<GetFlightStates, IReadOnlyList<FlightState>>
{
    private readonly IUnitOfWork _uow;
    public GetFlightStatesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<FlightState>> Handle(GetFlightStates _, CancellationToken ct)
        => _uow.Repository<FlightState>().GetAllAsync(ct);
}

public sealed record GetFlightStateById(int Id) : IRequest<FlightState?>;
public sealed class GetFlightStateByIdHandler : IRequestHandler<GetFlightStateById, FlightState?>
{
    private readonly IUnitOfWork _uow;
    public GetFlightStateByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<FlightState?> Handle(GetFlightStateById req, CancellationToken ct)
        => _uow.Repository<FlightState>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateFlightState(string Nombre) : IRequest<int>;
public sealed class CreateFlightStateHandler : IRequestHandler<CreateFlightState, int>
{
    private readonly IUnitOfWork _uow;
    public CreateFlightStateHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateFlightState req, CancellationToken ct)
    {
        var item = new FlightState(req.Nombre);
        await _uow.Repository<FlightState>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateFlightStateValidator : AbstractValidator<CreateFlightState>
{
    public CreateFlightStateValidator() => RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
}

public sealed record UpdateFlightStateName(int Id, string Nombre) : IRequest;
public sealed class UpdateFlightStateNameHandler : IRequestHandler<UpdateFlightStateName>
{
    private readonly IUnitOfWork _uow;
    public UpdateFlightStateNameHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateFlightStateName req, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightState>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"FlightState {req.Id} not found.");
        item.Update(req.Nombre);
        await _uow.Repository<FlightState>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteFlightState(int Id) : IRequest;
public sealed class DeleteFlightStateHandler : IRequestHandler<DeleteFlightState>
{
    private readonly IUnitOfWork _uow;
    public DeleteFlightStateHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteFlightState req, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightState>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"FlightState {req.Id} not found.");
        await _uow.Repository<FlightState>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}