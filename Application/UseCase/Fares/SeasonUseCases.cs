using Application.Abstractions;
using Domain.Entities.Fares;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Fares;

public sealed record GetSeasons : IRequest<IReadOnlyList<Season>>;
public sealed class GetSeasonsHandler : IRequestHandler<GetSeasons, IReadOnlyList<Season>>
{
    private readonly IUnitOfWork _uow;
    public GetSeasonsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<Season>> Handle(GetSeasons _, CancellationToken ct)
        => _uow.Repository<Season>().GetAllAsync(ct);
}

public sealed record GetSeasonById(int Id) : IRequest<Season?>;
public sealed class GetSeasonByIdHandler : IRequestHandler<GetSeasonById, Season?>
{
    private readonly IUnitOfWork _uow;
    public GetSeasonByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<Season?> Handle(GetSeasonById req, CancellationToken ct)
        => _uow.Repository<Season>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateSeason(string Nombre, decimal PrecioFactor, string? Descripcion) : IRequest<int>;
public sealed class CreateSeasonHandler : IRequestHandler<CreateSeason, int>
{
    private readonly IUnitOfWork _uow;
    public CreateSeasonHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateSeason req, CancellationToken ct)
    {
        var item = new Season(req.Nombre, req.PrecioFactor, req.Descripcion);
        await _uow.Repository<Season>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateSeasonValidator : AbstractValidator<CreateSeason>
{
    public CreateSeasonValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PrecioFactor).GreaterThan(0);
    }
}

public sealed record UpdateSeason(int Id, string Nombre, decimal PrecioFactor, string? Descripcion) : IRequest;
public sealed class UpdateSeasonHandler : IRequestHandler<UpdateSeason>
{
    private readonly IUnitOfWork _uow;
    public UpdateSeasonHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateSeason req, CancellationToken ct)
    {
        var item = await _uow.Repository<Season>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Season {req.Id} not found.");
        item.Update(req.Nombre, req.PrecioFactor, req.Descripcion);
        await _uow.Repository<Season>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteSeason(int Id) : IRequest;
public sealed class DeleteSeasonHandler : IRequestHandler<DeleteSeason>
{
    private readonly IUnitOfWork _uow;
    public DeleteSeasonHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteSeason req, CancellationToken ct)
    {
        var item = await _uow.Repository<Season>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Season {req.Id} not found.");
        await _uow.Repository<Season>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}