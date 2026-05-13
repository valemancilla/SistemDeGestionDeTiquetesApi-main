using Application.Abstractions;
using Domain.Entities.Fares;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Fares;

public sealed record GetFares : IRequest<IReadOnlyList<Fare>>;
public sealed class GetFaresHandler : IRequestHandler<GetFares, IReadOnlyList<Fare>>
{
    private readonly IUnitOfWork _uow;
    public GetFaresHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<Fare>> Handle(GetFares _, CancellationToken ct)
        => _uow.Repository<Fare>().GetAllAsync(ct);
}

public sealed record GetFareById(int Id) : IRequest<Fare?>;
public sealed class GetFareByIdHandler : IRequestHandler<GetFareById, Fare?>
{
    private readonly IUnitOfWork _uow;
    public GetFareByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<Fare?> Handle(GetFareById req, CancellationToken ct)
        => _uow.Repository<Fare>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateFare(
    int RutaId, int TipoCabinaId, int TipoPasajeroId, int TemporadaId,
    decimal PrecioBase, DateOnly? VigenciaDesde, DateOnly? VigenciaHasta) : IRequest<int>;
public sealed class CreateFareHandler : IRequestHandler<CreateFare, int>
{
    private readonly IUnitOfWork _uow;
    public CreateFareHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateFare req, CancellationToken ct)
    {
        var item = new Fare(req.RutaId, req.TipoCabinaId, req.TipoPasajeroId,
            req.TemporadaId, req.PrecioBase, req.VigenciaDesde, req.VigenciaHasta);
        await _uow.Repository<Fare>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateFareValidator : AbstractValidator<CreateFare>
{
    public CreateFareValidator()
    {
        RuleFor(x => x.RutaId).GreaterThan(0);
        RuleFor(x => x.TipoCabinaId).GreaterThan(0);
        RuleFor(x => x.TipoPasajeroId).GreaterThan(0);
        RuleFor(x => x.TemporadaId).GreaterThan(0);
        RuleFor(x => x.PrecioBase).GreaterThan(0);
    }
}

public sealed record UpdateFare(int Id, decimal PrecioBase, DateOnly? VigenciaDesde, DateOnly? VigenciaHasta) : IRequest;
public sealed class UpdateFareHandler : IRequestHandler<UpdateFare>
{
    private readonly IUnitOfWork _uow;
    public UpdateFareHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateFare req, CancellationToken ct)
    {
        var item = await _uow.Repository<Fare>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Fare {req.Id} not found.");
        item.Update(req.PrecioBase, req.VigenciaDesde, req.VigenciaHasta);
        await _uow.Repository<Fare>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteFare(int Id) : IRequest;
public sealed class DeleteFareHandler : IRequestHandler<DeleteFare>
{
    private readonly IUnitOfWork _uow;
    public DeleteFareHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteFare req, CancellationToken ct)
    {
        var item = await _uow.Repository<Fare>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Fare {req.Id} not found.");
        await _uow.Repository<Fare>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}