using Application.Abstractions;
using Domain.Entities.Flights;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Flights;

public sealed record GetFlightAssignments : IRequest<IReadOnlyList<FlightAssignment>>;
public sealed class GetFlightAssignmentsHandler : IRequestHandler<GetFlightAssignments, IReadOnlyList<FlightAssignment>>
{
    private readonly IUnitOfWork _uow;
    public GetFlightAssignmentsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<FlightAssignment>> Handle(GetFlightAssignments _, CancellationToken ct)
        => _uow.Repository<FlightAssignment>().GetAllAsync(ct);
}

public sealed record GetFlightAssignmentById(int Id) : IRequest<FlightAssignment?>;
public sealed class GetFlightAssignmentByIdHandler : IRequestHandler<GetFlightAssignmentById, FlightAssignment?>
{
    private readonly IUnitOfWork _uow;
    public GetFlightAssignmentByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<FlightAssignment?> Handle(GetFlightAssignmentById req, CancellationToken ct)
        => _uow.Repository<FlightAssignment>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateFlightAssignment(int VueloId, int PersonalId, int RolVueloId) : IRequest<int>;
public sealed class CreateFlightAssignmentHandler : IRequestHandler<CreateFlightAssignment, int>
{
    private readonly IUnitOfWork _uow;
    public CreateFlightAssignmentHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateFlightAssignment req, CancellationToken ct)
    {
        var item = new FlightAssignment(req.VueloId, req.PersonalId, req.RolVueloId);
        await _uow.Repository<FlightAssignment>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateFlightAssignmentValidator : AbstractValidator<CreateFlightAssignment>
{
    public CreateFlightAssignmentValidator()
    {
        RuleFor(x => x.VueloId).GreaterThan(0);
        RuleFor(x => x.PersonalId).GreaterThan(0);
        RuleFor(x => x.RolVueloId).GreaterThan(0);
    }
}

public sealed record UpdateFlightAssignment(int Id, int RolVueloId) : IRequest;
public sealed class UpdateFlightAssignmentHandler : IRequestHandler<UpdateFlightAssignment>
{
    private readonly IUnitOfWork _uow;
    public UpdateFlightAssignmentHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateFlightAssignment req, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightAssignment>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"FlightAssignment {req.Id} not found.");
        item.Update(req.RolVueloId);
        await _uow.Repository<FlightAssignment>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteFlightAssignment(int Id) : IRequest;
public sealed class DeleteFlightAssignmentHandler : IRequestHandler<DeleteFlightAssignment>
{
    private readonly IUnitOfWork _uow;
    public DeleteFlightAssignmentHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteFlightAssignment req, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightAssignment>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"FlightAssignment {req.Id} not found.");
        await _uow.Repository<FlightAssignment>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}