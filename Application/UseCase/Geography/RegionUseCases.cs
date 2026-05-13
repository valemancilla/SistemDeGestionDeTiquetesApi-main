using Application.Abstractions;
using Domain.Entities.Geography;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Geography;

public sealed record GetRegions : IRequest<IReadOnlyList<Region>>;
public sealed class GetRegionsHandler : IRequestHandler<GetRegions, IReadOnlyList<Region>>
{
    private readonly IUnitOfWork _uow;
    public GetRegionsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<Region>> Handle(GetRegions _, CancellationToken ct)
        => _uow.Repository<Region>().GetAllAsync(ct);
}

public sealed record GetRegionById(int Id) : IRequest<Region?>;
public sealed class GetRegionByIdHandler : IRequestHandler<GetRegionById, Region?>
{
    private readonly IUnitOfWork _uow;
    public GetRegionByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<Region?> Handle(GetRegionById req, CancellationToken ct)
        => _uow.Repository<Region>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateRegion(string Nombre, string Tipo, int PaisId) : IRequest<int>;
public sealed class CreateRegionHandler : IRequestHandler<CreateRegion, int>
{
    private readonly IUnitOfWork _uow;
    public CreateRegionHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateRegion req, CancellationToken ct)
    {
        var item = new Region(req.Nombre, req.Tipo, req.PaisId);
        await _uow.Repository<Region>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateRegionValidator : AbstractValidator<CreateRegion>
{
    public CreateRegionValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Tipo).NotEmpty().MaximumLength(50);
        RuleFor(x => x.PaisId).GreaterThan(0);
    }
}

public sealed record UpdateRegion(int Id, string Nombre, string Tipo) : IRequest;
public sealed class UpdateRegionHandler : IRequestHandler<UpdateRegion>
{
    private readonly IUnitOfWork _uow;
    public UpdateRegionHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateRegion req, CancellationToken ct)
    {
        var item = await _uow.Repository<Region>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Region {req.Id} not found.");
        item.Update(req.Nombre, req.Tipo);
        await _uow.Repository<Region>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
public sealed class UpdateRegionValidator : AbstractValidator<UpdateRegion>
{
    public UpdateRegionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Tipo).NotEmpty().MaximumLength(50);
    }
}

public sealed record DeleteRegion(int Id) : IRequest;
public sealed class DeleteRegionHandler : IRequestHandler<DeleteRegion>
{
    private readonly IUnitOfWork _uow;
    public DeleteRegionHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteRegion req, CancellationToken ct)
    {
        var item = await _uow.Repository<Region>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Region {req.Id} not found.");
        await _uow.Repository<Region>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}