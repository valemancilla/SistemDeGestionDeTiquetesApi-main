using Application.Abstractions;
using Domain.Entities.Aircraft;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Aircraft;

public sealed record GetAircraftModels : IRequest<IReadOnlyList<AircraftModel>>;
public sealed class GetAircraftModelsHandler : IRequestHandler<GetAircraftModels, IReadOnlyList<AircraftModel>>
{
    private readonly IUnitOfWork _uow;
    public GetAircraftModelsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<AircraftModel>> Handle(GetAircraftModels _, CancellationToken ct)
        => _uow.Repository<AircraftModel>().GetAllAsync(ct);
}

public sealed record GetAircraftModelById(int Id) : IRequest<AircraftModel?>;
public sealed class GetAircraftModelByIdHandler : IRequestHandler<GetAircraftModelById, AircraftModel?>
{
    private readonly IUnitOfWork _uow;
    public GetAircraftModelByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<AircraftModel?> Handle(GetAircraftModelById req, CancellationToken ct)
        => _uow.Repository<AircraftModel>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateAircraftModel(
    int FabricanteId, string NombreModelo, int CapacidadMaxima,
    decimal? PesoMaxDespegueKg, decimal? ConsumoCombustibleKgH,
    int? VelocidadCruceroKmh, int? AltitudCruceroFt) : IRequest<int>;
public sealed class CreateAircraftModelHandler : IRequestHandler<CreateAircraftModel, int>
{
    private readonly IUnitOfWork _uow;
    public CreateAircraftModelHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateAircraftModel req, CancellationToken ct)
    {
        var item = new AircraftModel(req.FabricanteId, req.NombreModelo, req.CapacidadMaxima,
            req.PesoMaxDespegueKg, req.ConsumoCombustibleKgH, req.VelocidadCruceroKmh, req.AltitudCruceroFt);
        await _uow.Repository<AircraftModel>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateAircraftModelValidator : AbstractValidator<CreateAircraftModel>
{
    public CreateAircraftModelValidator()
    {
        RuleFor(x => x.FabricanteId).GreaterThan(0);
        RuleFor(x => x.NombreModelo).NotEmpty().MaximumLength(100);
        RuleFor(x => x.CapacidadMaxima).GreaterThan(0);
    }
}

public sealed record UpdateAircraftModel(
    int Id, string NombreModelo, int CapacidadMaxima,
    decimal? PesoMaxDespegueKg, decimal? ConsumoCombustibleKgH,
    int? VelocidadCruceroKmh, int? AltitudCruceroFt) : IRequest;
public sealed class UpdateAircraftModelHandler : IRequestHandler<UpdateAircraftModel>
{
    private readonly IUnitOfWork _uow;
    public UpdateAircraftModelHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateAircraftModel req, CancellationToken ct)
    {
        var item = await _uow.Repository<AircraftModel>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"AircraftModel {req.Id} not found.");
        item.Update(req.NombreModelo, req.CapacidadMaxima, req.PesoMaxDespegueKg,
            req.ConsumoCombustibleKgH, req.VelocidadCruceroKmh, req.AltitudCruceroFt);
        await _uow.Repository<AircraftModel>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
public sealed class UpdateAircraftModelValidator : AbstractValidator<UpdateAircraftModel>
{
    public UpdateAircraftModelValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.NombreModelo).NotEmpty().MaximumLength(100);
        RuleFor(x => x.CapacidadMaxima).GreaterThan(0);
    }
}

public sealed record DeleteAircraftModel(int Id) : IRequest;
public sealed class DeleteAircraftModelHandler : IRequestHandler<DeleteAircraftModel>
{
    private readonly IUnitOfWork _uow;
    public DeleteAircraftModelHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteAircraftModel req, CancellationToken ct)
    {
        var item = await _uow.Repository<AircraftModel>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"AircraftModel {req.Id} not found.");
        await _uow.Repository<AircraftModel>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}