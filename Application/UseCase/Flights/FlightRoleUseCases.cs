using Application.Abstractions;
using Domain.Entities.Flights;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Flights;

public sealed record GetFlightRoles : IRequest<IReadOnlyList<FlightRole>>;
public sealed class GetFlightRolesHandler : IRequestHandler<GetFlightRoles, IReadOnlyList<FlightRole>>
{
    private readonly IUnitOfWork _uow;
    public GetFlightRolesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<FlightRole>> Handle(GetFlightRoles _, CancellationToken ct)
        => _uow.Repository<FlightRole>().GetAllAsync(ct);
}

public sealed record GetFlightRoleById(int Id) : IRequest<FlightRole?>;
public sealed class GetFlightRoleByIdHandler : IRequestHandler<GetFlightRoleById, FlightRole?>
{
    private readonly IUnitOfWork _uow;
    public GetFlightRoleByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<FlightRole?> Handle(GetFlightRoleById req, CancellationToken ct)
        => _uow.Repository<FlightRole>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateFlightRole(string Nombre) : IRequest<int>;
public sealed class CreateFlightRoleHandler : IRequestHandler<CreateFlightRole, int>
{
    private readonly IUnitOfWork _uow;
    public CreateFlightRoleHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateFlightRole req, CancellationToken ct)
    {
        var item = new FlightRole(req.Nombre);
        await _uow.Repository<FlightRole>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateFlightRoleValidator : AbstractValidator<CreateFlightRole>
{
    public CreateFlightRoleValidator() => RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
}

public sealed record UpdateFlightRole(int Id, string Nombre) : IRequest;
public sealed class UpdateFlightRoleHandler : IRequestHandler<UpdateFlightRole>
{
    private readonly IUnitOfWork _uow;
    public UpdateFlightRoleHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateFlightRole req, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightRole>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"FlightRole {req.Id} not found.");
        item.Update(req.Nombre);
        await _uow.Repository<FlightRole>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteFlightRole(int Id) : IRequest;
public sealed class DeleteFlightRoleHandler : IRequestHandler<DeleteFlightRole>
{
    private readonly IUnitOfWork _uow;
    public DeleteFlightRoleHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteFlightRole req, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightRole>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"FlightRole {req.Id} not found.");
        await _uow.Repository<FlightRole>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}