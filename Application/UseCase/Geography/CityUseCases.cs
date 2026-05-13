using Application.Abstractions;
using Domain.Entities.Geography;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Geography;

public sealed record GetCities : IRequest<IReadOnlyList<City>>;
public sealed class GetCitiesHandler : IRequestHandler<GetCities, IReadOnlyList<City>>
{
    private readonly IUnitOfWork _uow;
    public GetCitiesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<City>> Handle(GetCities _, CancellationToken ct)
        => _uow.Repository<City>().GetAllAsync(ct);
}

public sealed record GetCityById(int Id) : IRequest<City?>;
public sealed class GetCityByIdHandler : IRequestHandler<GetCityById, City?>
{
    private readonly IUnitOfWork _uow;
    public GetCityByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<City?> Handle(GetCityById req, CancellationToken ct)
        => _uow.Repository<City>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateCity(string Nombre, int RegionId) : IRequest<int>;
public sealed class CreateCityHandler : IRequestHandler<CreateCity, int>
{
    private readonly IUnitOfWork _uow;
    public CreateCityHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateCity req, CancellationToken ct)
    {
        var item = new City(req.Nombre, req.RegionId);
        await _uow.Repository<City>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateCityValidator : AbstractValidator<CreateCity>
{
    public CreateCityValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.RegionId).GreaterThan(0);
    }
}

public sealed record UpdateCity(int Id, string Nombre) : IRequest;
public sealed class UpdateCityHandler : IRequestHandler<UpdateCity>
{
    private readonly IUnitOfWork _uow;
    public UpdateCityHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateCity req, CancellationToken ct)
    {
        var item = await _uow.Repository<City>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"City {req.Id} not found.");
        item.Update(req.Nombre);
        await _uow.Repository<City>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
public sealed class UpdateCityValidator : AbstractValidator<UpdateCity>
{
    public UpdateCityValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
    }
}

public sealed record DeleteCity(int Id) : IRequest;
public sealed class DeleteCityHandler : IRequestHandler<DeleteCity>
{
    private readonly IUnitOfWork _uow;
    public DeleteCityHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteCity req, CancellationToken ct)
    {
        var item = await _uow.Repository<City>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"City {req.Id} not found.");
        await _uow.Repository<City>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}