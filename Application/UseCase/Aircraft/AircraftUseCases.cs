using Application.Abstractions;
using Domain.Entities.Aircraft;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Aircraft;

public sealed record GetAircrafts : IRequest<IReadOnlyList<Domain.Entities.Aircraft.Aircraft>>;
public sealed class GetAircraftsHandler : IRequestHandler<GetAircrafts, IReadOnlyList<Domain.Entities.Aircraft.Aircraft>>
{
    private readonly IUnitOfWork _uow;
    public GetAircraftsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<Domain.Entities.Aircraft.Aircraft>> Handle(GetAircrafts _, CancellationToken ct)
        => _uow.Aircraft.GetAllAsync(ct);
}

public sealed record GetAircraftById(int Id) : IRequest<Domain.Entities.Aircraft.Aircraft?>;
public sealed class GetAircraftByIdHandler : IRequestHandler<GetAircraftById, Domain.Entities.Aircraft.Aircraft?>
{
    private readonly IUnitOfWork _uow;
    public GetAircraftByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<Domain.Entities.Aircraft.Aircraft?> Handle(GetAircraftById req, CancellationToken ct)
        => _uow.Aircraft.GetByIdAsync(req.Id, ct);
}

public sealed record GetAircraftsByAerolinea(int AerolineaId) : IRequest<IReadOnlyList<Domain.Entities.Aircraft.Aircraft>>;
public sealed class GetAircraftsByAerolineaHandler : IRequestHandler<GetAircraftsByAerolinea, IReadOnlyList<Domain.Entities.Aircraft.Aircraft>>
{
    private readonly IUnitOfWork _uow;
    public GetAircraftsByAerolineaHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<Domain.Entities.Aircraft.Aircraft>> Handle(GetAircraftsByAerolinea req, CancellationToken ct)
        => _uow.Aircraft.GetByAerolineaAsync(req.AerolineaId, ct);
}

public sealed record CreateAircraftCommand(int ModeloId, int AerolineaId, string Matricula, DateOnly? FechaFabricacion) : IRequest<int>;
public sealed class CreateAircraftCommandHandler : IRequestHandler<CreateAircraftCommand, int>
{
    private readonly IUnitOfWork _uow;
    public CreateAircraftCommandHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateAircraftCommand req, CancellationToken ct)
    {
        if (await _uow.Aircraft.ExistsMatriculaAsync(req.Matricula, ct))
            throw new InvalidOperationException($"Ya existe una aeronave con matrícula '{req.Matricula}'.");
        var item = new Domain.Entities.Aircraft.Aircraft(req.ModeloId, req.AerolineaId, req.Matricula, req.FechaFabricacion);
        await _uow.Aircraft.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateAircraftCommandValidator : AbstractValidator<CreateAircraftCommand>
{
    public CreateAircraftCommandValidator()
    {
        RuleFor(x => x.ModeloId).GreaterThan(0);
        RuleFor(x => x.AerolineaId).GreaterThan(0);
        RuleFor(x => x.Matricula).NotEmpty().Length(1, 20).Matches("^[A-Za-z0-9\\-]+$");
    }
}

public sealed record UpdateAircraftCommand(int Id, int ModeloId, int AerolineaId, DateOnly? FechaFabricacion, bool Activa) : IRequest;
public sealed class UpdateAircraftCommandHandler : IRequestHandler<UpdateAircraftCommand>
{
    private readonly IUnitOfWork _uow;
    public UpdateAircraftCommandHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateAircraftCommand req, CancellationToken ct)
    {
        var item = await _uow.Aircraft.GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Aircraft {req.Id} not found.");
        item.Update(req.ModeloId, req.AerolineaId, req.FechaFabricacion, req.Activa);
        await _uow.Aircraft.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteAircraftCommand(int Id) : IRequest;
public sealed class DeleteAircraftCommandHandler : IRequestHandler<DeleteAircraftCommand>
{
    private readonly IUnitOfWork _uow;
    public DeleteAircraftCommandHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteAircraftCommand req, CancellationToken ct)
    {
        var item = await _uow.Aircraft.GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Aircraft {req.Id} not found.");
        await _uow.Repository<Domain.Entities.Aircraft.Aircraft>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}