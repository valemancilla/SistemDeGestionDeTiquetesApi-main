using Application.Abstractions;
using Domain.Entities.People;
using FluentValidation;
using MediatR;

namespace Application.UseCase.People;

public sealed record GetPersonEmails : IRequest<IReadOnlyList<PersonEmail>>;
public sealed class GetPersonEmailsHandler : IRequestHandler<GetPersonEmails, IReadOnlyList<PersonEmail>>
{
    private readonly IUnitOfWork _uow;
    public GetPersonEmailsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<PersonEmail>> Handle(GetPersonEmails _, CancellationToken ct)
        => _uow.Repository<PersonEmail>().GetAllAsync(ct);
}

public sealed record GetPersonEmailById(int Id) : IRequest<PersonEmail?>;
public sealed class GetPersonEmailByIdHandler : IRequestHandler<GetPersonEmailById, PersonEmail?>
{
    private readonly IUnitOfWork _uow;
    public GetPersonEmailByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<PersonEmail?> Handle(GetPersonEmailById req, CancellationToken ct)
        => _uow.Repository<PersonEmail>().GetByIdAsync(req.Id, ct);
}

public sealed record CreatePersonEmail(int PersonaId, string UsuarioEmail, int DominioEmailId, bool EsPrincipal) : IRequest<int>;
public sealed class CreatePersonEmailHandler : IRequestHandler<CreatePersonEmail, int>
{
    private readonly IUnitOfWork _uow;
    public CreatePersonEmailHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreatePersonEmail req, CancellationToken ct)
    {
        var item = new PersonEmail(req.PersonaId, req.UsuarioEmail, req.DominioEmailId, req.EsPrincipal);
        await _uow.Repository<PersonEmail>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreatePersonEmailValidator : AbstractValidator<CreatePersonEmail>
{
    public CreatePersonEmailValidator()
    {
        RuleFor(x => x.PersonaId).GreaterThan(0);
        RuleFor(x => x.UsuarioEmail).NotEmpty().Length(1, 100).Matches("^[a-zA-Z0-9](?:[a-zA-Z0-9._+-]{0,98}[a-zA-Z0-9])?$");
        RuleFor(x => x.DominioEmailId).GreaterThan(0);
    }
}

public sealed record UpdatePersonEmail(int Id, string UsuarioEmail, int DominioEmailId, bool EsPrincipal) : IRequest;
public sealed class UpdatePersonEmailHandler : IRequestHandler<UpdatePersonEmail>
{
    private readonly IUnitOfWork _uow;
    public UpdatePersonEmailHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdatePersonEmail req, CancellationToken ct)
    {
        var item = await _uow.Repository<PersonEmail>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"PersonEmail {req.Id} not found.");
        item.Update(req.UsuarioEmail, req.DominioEmailId, req.EsPrincipal);
        await _uow.Repository<PersonEmail>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
public sealed class UpdatePersonEmailValidator : AbstractValidator<UpdatePersonEmail>
{
    public UpdatePersonEmailValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.UsuarioEmail).NotEmpty().Length(1, 100).Matches("^[a-zA-Z0-9](?:[a-zA-Z0-9._+-]{0,98}[a-zA-Z0-9])?$");
        RuleFor(x => x.DominioEmailId).GreaterThan(0);
    }
}

public sealed record DeletePersonEmail(int Id) : IRequest;
public sealed class DeletePersonEmailHandler : IRequestHandler<DeletePersonEmail>
{
    private readonly IUnitOfWork _uow;
    public DeletePersonEmailHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeletePersonEmail req, CancellationToken ct)
    {
        var item = await _uow.Repository<PersonEmail>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"PersonEmail {req.Id} not found.");
        await _uow.Repository<PersonEmail>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}