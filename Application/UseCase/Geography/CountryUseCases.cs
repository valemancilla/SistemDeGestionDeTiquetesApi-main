using Application.Abstractions;
using Domain.Entities.Geography;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Geography;

public sealed record GetCountries : IRequest<IReadOnlyList<Country>>;
public sealed class GetCountriesHandler : IRequestHandler<GetCountries, IReadOnlyList<Country>>
{
    private readonly IUnitOfWork _uow;
    public GetCountriesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<Country>> Handle(GetCountries _, CancellationToken ct)
        => _uow.Repository<Country>().GetAllAsync(ct);
}

public sealed record GetCountryById(int Id) : IRequest<Country?>;
public sealed class GetCountryByIdHandler : IRequestHandler<GetCountryById, Country?>
{
    private readonly IUnitOfWork _uow;
    public GetCountryByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<Country?> Handle(GetCountryById req, CancellationToken ct)
        => _uow.Repository<Country>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateCountry(string Nombre, string CodigoIso, int ContinenteId) : IRequest<int>;
public sealed class CreateCountryHandler : IRequestHandler<CreateCountry, int>
{
    private readonly IUnitOfWork _uow;
    public CreateCountryHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateCountry req, CancellationToken ct)
    {
        var item = new Country(req.Nombre, req.CodigoIso, req.ContinenteId);
        await _uow.Repository<Country>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateCountryValidator : AbstractValidator<CreateCountry>
{
    public CreateCountryValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.CodigoIso).NotEmpty().Length(2, 3).Matches("^[A-Za-z]{2,3}$");
        RuleFor(x => x.ContinenteId).GreaterThan(0);
    }
}

public sealed record UpdateCountry(int Id, string Nombre, string CodigoIso) : IRequest;
public sealed class UpdateCountryHandler : IRequestHandler<UpdateCountry>
{
    private readonly IUnitOfWork _uow;
    public UpdateCountryHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateCountry req, CancellationToken ct)
    {
        var item = await _uow.Repository<Country>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Country {req.Id} not found.");
        item.Update(req.Nombre, req.CodigoIso);
        await _uow.Repository<Country>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
public sealed class UpdateCountryValidator : AbstractValidator<UpdateCountry>
{
    public UpdateCountryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.CodigoIso).NotEmpty().Length(2, 3).Matches("^[A-Za-z]{2,3}$");
    }
}

public sealed record DeleteCountry(int Id) : IRequest;
public sealed class DeleteCountryHandler : IRequestHandler<DeleteCountry>
{
    private readonly IUnitOfWork _uow;
    public DeleteCountryHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteCountry req, CancellationToken ct)
    {
        var item = await _uow.Repository<Country>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Country {req.Id} not found.");
        await _uow.Repository<Country>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}