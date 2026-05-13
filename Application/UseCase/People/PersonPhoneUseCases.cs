using Application.Abstractions;
using Domain.Entities.People;
using FluentValidation;
using MediatR;

namespace Application.UseCase.People;

public sealed record GetPersonPhones : IRequest<IReadOnlyList<PersonPhone>>;
public sealed class GetPersonPhonesHandler : IRequestHandler<GetPersonPhones, IReadOnlyList<PersonPhone>>
{
    private readonly IUnitOfWork _uow;
    public GetPersonPhonesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<PersonPhone>> Handle(GetPersonPhones _, CancellationToken ct)
        => _uow.Repository<PersonPhone>().GetAllAsync(ct);
}

public sealed record GetPersonPhoneById(int Id) : IRequest<PersonPhone?>;
public sealed class GetPersonPhoneByIdHandler : IRequestHandler<GetPersonPhoneById, PersonPhone?>
{
    private readonly IUnitOfWork _uow;
    public GetPersonPhoneByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<PersonPhone?> Handle(GetPersonPhoneById req, CancellationToken ct)
        => _uow.Repository<PersonPhone>().GetByIdAsync(req.Id, ct);
}

public sealed record CreatePersonPhone(int PersonaId, int CodigoTelefonoId, string NumeroTelefono, bool EsPrincipal) : IRequest<int>;
public sealed class CreatePersonPhoneHandler : IRequestHandler<CreatePersonPhone, int>
{
    private readonly IUnitOfWork _uow;
    public CreatePersonPhoneHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreatePersonPhone req, CancellationToken ct)
    {
        var item = new PersonPhone(req.PersonaId, req.CodigoTelefonoId, req.NumeroTelefono, req.EsPrincipal);
        await _uow.Repository<PersonPhone>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreatePersonPhoneValidator : AbstractValidator<CreatePersonPhone>
{
    public CreatePersonPhoneValidator()
    {
        RuleFor(x => x.PersonaId).GreaterThan(0);
        RuleFor(x => x.CodigoTelefonoId).GreaterThan(0);
        RuleFor(x => x.NumeroTelefono).NotEmpty().Length(1, 20).Matches("^[0-9 \\-]+$");
    }
}

public sealed record UpdatePersonPhone(int Id, int CodigoTelefonoId, string NumeroTelefono, bool EsPrincipal) : IRequest;
public sealed class UpdatePersonPhoneHandler : IRequestHandler<UpdatePersonPhone>
{
    private readonly IUnitOfWork _uow;
    public UpdatePersonPhoneHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdatePersonPhone req, CancellationToken ct)
    {
        var item = await _uow.Repository<PersonPhone>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"PersonPhone {req.Id} not found.");
        item.Update(req.CodigoTelefonoId, req.NumeroTelefono, req.EsPrincipal);
        await _uow.Repository<PersonPhone>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
public sealed class UpdatePersonPhoneValidator : AbstractValidator<UpdatePersonPhone>
{
    public UpdatePersonPhoneValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.CodigoTelefonoId).GreaterThan(0);
        RuleFor(x => x.NumeroTelefono).NotEmpty().Length(1, 20).Matches("^[0-9 \\-]+$");
    }
}

public sealed record DeletePersonPhone(int Id) : IRequest;
public sealed class DeletePersonPhoneHandler : IRequestHandler<DeletePersonPhone>
{
    private readonly IUnitOfWork _uow;
    public DeletePersonPhoneHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeletePersonPhone req, CancellationToken ct)
    {
        var item = await _uow.Repository<PersonPhone>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"PersonPhone {req.Id} not found.");
        await _uow.Repository<PersonPhone>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}