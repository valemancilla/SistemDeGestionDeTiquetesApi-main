using Application.Abstractions;
using Domain.Entities.Geography;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Geography;

public sealed record GetContinents : IRequest<IReadOnlyList<Continent>>;
public sealed class GetContinentsHandler : IRequestHandler<GetContinents, IReadOnlyList<Continent>>
{
    private readonly IUnitOfWork _uow;
    public GetContinentsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<Continent>> Handle(GetContinents _, CancellationToken ct)
        => _uow.Repository<Continent>().GetAllAsync(ct);
}

public sealed record GetContinentById(int Id) : IRequest<Continent?>;
public sealed class GetContinentByIdHandler : IRequestHandler<GetContinentById, Continent?>
{
    private readonly IUnitOfWork _uow;
    public GetContinentByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<Continent?> Handle(GetContinentById req, CancellationToken ct)
        => _uow.Repository<Continent>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateContinent(string Nombre) : IRequest<int>;
public sealed class CreateContinentHandler : IRequestHandler<CreateContinent, int>
{
    private readonly IUnitOfWork _uow;
    public CreateContinentHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateContinent req, CancellationToken ct)
    {
        var item = new Continent(req.Nombre);
        await _uow.Repository<Continent>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateContinentValidator : AbstractValidator<CreateContinent>
{
    public CreateContinentValidator() => RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
}

public sealed record UpdateContinent(int Id, string Nombre) : IRequest;
public sealed class UpdateContinentHandler : IRequestHandler<UpdateContinent>
{
    private readonly IUnitOfWork _uow;
    public UpdateContinentHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateContinent req, CancellationToken ct)
    {
        var item = await _uow.Repository<Continent>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Continent {req.Id} not found.");
        item.Update(req.Nombre);
        await _uow.Repository<Continent>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
public sealed class UpdateContinentValidator : AbstractValidator<UpdateContinent>
{
    public UpdateContinentValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
    }
}

public sealed record DeleteContinent(int Id) : IRequest;
public sealed class DeleteContinentHandler : IRequestHandler<DeleteContinent>
{
    private readonly IUnitOfWork _uow;
    public DeleteContinentHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteContinent req, CancellationToken ct)
    {
        var item = await _uow.Repository<Continent>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Continent {req.Id} not found.");
        await _uow.Repository<Continent>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}