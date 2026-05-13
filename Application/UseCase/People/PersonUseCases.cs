using Application.Abstractions;
using Domain.Entities.People;
using FluentValidation;
using MediatR;

namespace Application.UseCase.People;

public sealed record GetPersons : IRequest<IReadOnlyList<Person>>;
public sealed class GetPersonsHandler : IRequestHandler<GetPersons, IReadOnlyList<Person>>
{
    private readonly IUnitOfWork _uow;
    public GetPersonsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<Person>> Handle(GetPersons _, CancellationToken ct)
        => _uow.Repository<Person>().GetAllAsync(ct);
}

public sealed record GetPersonById(int Id) : IRequest<Person?>;
public sealed class GetPersonByIdHandler : IRequestHandler<GetPersonById, Person?>
{
    private readonly IUnitOfWork _uow;
    public GetPersonByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<Person?> Handle(GetPersonById req, CancellationToken ct)
        => _uow.Repository<Person>().GetByIdAsync(req.Id, ct);
}

public sealed record CreatePerson(
    int TipoDocumentoId, string NumeroDocumento, string Nombres, string Apellidos,
    DateOnly? FechaNacimiento, char? Genero, int? DireccionId) : IRequest<int>;
public sealed class CreatePersonHandler : IRequestHandler<CreatePerson, int>
{
    private readonly IUnitOfWork _uow;
    public CreatePersonHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreatePerson req, CancellationToken ct)
    {
        var item = new Person(req.TipoDocumentoId, req.NumeroDocumento, req.Nombres,
            req.Apellidos, req.FechaNacimiento, req.Genero, req.DireccionId);
        await _uow.Repository<Person>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreatePersonValidator : AbstractValidator<CreatePerson>
{
    public CreatePersonValidator()
    {
        RuleFor(x => x.TipoDocumentoId).GreaterThan(0);
        RuleFor(x => x.NumeroDocumento).NotEmpty().Length(1, 30).Matches("^[A-Za-z0-9\\-]+$");
        RuleFor(x => x.Nombres).NotEmpty().Length(1, 100);
        RuleFor(x => x.Apellidos).NotEmpty().Length(1, 100);
    }
}

public sealed record UpdatePerson(int Id, string Nombres, string Apellidos, char? Genero, int? DireccionId) : IRequest;
public sealed class UpdatePersonHandler : IRequestHandler<UpdatePerson>
{
    private readonly IUnitOfWork _uow;
    public UpdatePersonHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdatePerson req, CancellationToken ct)
    {
        var item = await _uow.Repository<Person>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Person {req.Id} not found.");
        item.Update(req.Nombres, req.Apellidos, req.Genero, req.DireccionId);
        await _uow.Repository<Person>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
public sealed class UpdatePersonValidator : AbstractValidator<UpdatePerson>
{
    public UpdatePersonValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Nombres).NotEmpty().Length(1, 100);
        RuleFor(x => x.Apellidos).NotEmpty().Length(1, 100);
    }
}

public sealed record DeletePerson(int Id) : IRequest;
public sealed class DeletePersonHandler : IRequestHandler<DeletePerson>
{
    private readonly IUnitOfWork _uow;
    public DeletePersonHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeletePerson req, CancellationToken ct)
    {
        var item = await _uow.Repository<Person>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Person {req.Id} not found.");
        await _uow.Repository<Person>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}