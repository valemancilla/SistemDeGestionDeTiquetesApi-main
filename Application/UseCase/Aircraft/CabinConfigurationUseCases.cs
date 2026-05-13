using Application.Abstractions;
using Domain.Entities.Aircraft;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Aircraft;

public sealed record GetCabinConfigurations : IRequest<IReadOnlyList<CabinConfiguration>>;
public sealed class GetCabinConfigurationsHandler : IRequestHandler<GetCabinConfigurations, IReadOnlyList<CabinConfiguration>>
{
    private readonly IUnitOfWork _uow;
    public GetCabinConfigurationsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<CabinConfiguration>> Handle(GetCabinConfigurations _, CancellationToken ct)
        => _uow.Repository<CabinConfiguration>().GetAllAsync(ct);
}

public sealed record GetCabinConfigurationById(int Id) : IRequest<CabinConfiguration?>;
public sealed class GetCabinConfigurationByIdHandler : IRequestHandler<GetCabinConfigurationById, CabinConfiguration?>
{
    private readonly IUnitOfWork _uow;
    public GetCabinConfigurationByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<CabinConfiguration?> Handle(GetCabinConfigurationById req, CancellationToken ct)
        => _uow.Repository<CabinConfiguration>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateCabinConfiguration(
    int AeronaveId, int TipoCabinaId, int FilaInicio, int FilaFin,
    int AsientosPorFila, string LetrasAsientos) : IRequest<int>;
public sealed class CreateCabinConfigurationHandler : IRequestHandler<CreateCabinConfiguration, int>
{
    private readonly IUnitOfWork _uow;
    public CreateCabinConfigurationHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateCabinConfiguration req, CancellationToken ct)
    {
        var item = new CabinConfiguration(req.AeronaveId, req.TipoCabinaId, req.FilaInicio,
            req.FilaFin, req.AsientosPorFila, req.LetrasAsientos);
        await _uow.Repository<CabinConfiguration>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateCabinConfigurationValidator : AbstractValidator<CreateCabinConfiguration>
{
    public CreateCabinConfigurationValidator()
    {
        RuleFor(x => x.AeronaveId).GreaterThan(0);
        RuleFor(x => x.TipoCabinaId).GreaterThan(0);
        RuleFor(x => x.FilaInicio).GreaterThan(0);
        RuleFor(x => x.FilaFin).GreaterThan(0);
        RuleFor(x => x.AsientosPorFila).GreaterThan(0);
        RuleFor(x => x.LetrasAsientos).NotEmpty().Length(1, 10).Matches("^[A-Za-z]+$");
    }
}

public sealed record UpdateCabinConfiguration(
    int Id, int FilaInicio, int FilaFin, int AsientosPorFila, string LetrasAsientos) : IRequest;
public sealed class UpdateCabinConfigurationHandler : IRequestHandler<UpdateCabinConfiguration>
{
    private readonly IUnitOfWork _uow;
    public UpdateCabinConfigurationHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateCabinConfiguration req, CancellationToken ct)
    {
        var item = await _uow.Repository<CabinConfiguration>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"CabinConfiguration {req.Id} not found.");
        item.Update(req.FilaInicio, req.FilaFin, req.AsientosPorFila, req.LetrasAsientos);
        await _uow.Repository<CabinConfiguration>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed class UpdateCabinConfigurationValidator : AbstractValidator<UpdateCabinConfiguration>
{
    public UpdateCabinConfigurationValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.FilaInicio).GreaterThan(0);
        RuleFor(x => x.FilaFin).GreaterThan(0);
        RuleFor(x => x.AsientosPorFila).GreaterThan(0);
        RuleFor(x => x.LetrasAsientos).NotEmpty().Length(1, 10).Matches("^[A-Za-z]+$");
    }
}

public sealed record DeleteCabinConfiguration(int Id) : IRequest;
public sealed class DeleteCabinConfigurationHandler : IRequestHandler<DeleteCabinConfiguration>
{
    private readonly IUnitOfWork _uow;
    public DeleteCabinConfigurationHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteCabinConfiguration req, CancellationToken ct)
    {
        var item = await _uow.Repository<CabinConfiguration>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"CabinConfiguration {req.Id} not found.");
        await _uow.Repository<CabinConfiguration>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}