using Application.Abstractions;
using Domain.Entities.People;
using FluentValidation;
using MediatR;

namespace Application.UseCase.People;

public sealed record GetEmailDomains : IRequest<IReadOnlyList<EmailDomain>>;
public sealed class GetEmailDomainsHandler : IRequestHandler<GetEmailDomains, IReadOnlyList<EmailDomain>>
{
    private readonly IUnitOfWork _uow;
    public GetEmailDomainsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<EmailDomain>> Handle(GetEmailDomains _, CancellationToken ct)
        => _uow.Repository<EmailDomain>().GetAllAsync(ct);
}

public sealed record GetEmailDomainById(int Id) : IRequest<EmailDomain?>;
public sealed class GetEmailDomainByIdHandler : IRequestHandler<GetEmailDomainById, EmailDomain?>
{
    private readonly IUnitOfWork _uow;
    public GetEmailDomainByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<EmailDomain?> Handle(GetEmailDomainById req, CancellationToken ct)
        => _uow.Repository<EmailDomain>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateEmailDomain(string Dominio) : IRequest<int>;
public sealed class CreateEmailDomainHandler : IRequestHandler<CreateEmailDomain, int>
{
    private readonly IUnitOfWork _uow;
    public CreateEmailDomainHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateEmailDomain req, CancellationToken ct)
    {
        var item = new EmailDomain(req.Dominio);
        await _uow.Repository<EmailDomain>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateEmailDomainValidator : AbstractValidator<CreateEmailDomain>
{
    public CreateEmailDomainValidator() =>
        RuleFor(x => x.Dominio).NotEmpty().Length(4, 100).Matches(@"^(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,63}$");
}

public sealed record UpdateEmailDomain(int Id, string Dominio) : IRequest;
public sealed class UpdateEmailDomainHandler : IRequestHandler<UpdateEmailDomain>
{
    private readonly IUnitOfWork _uow;
    public UpdateEmailDomainHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateEmailDomain req, CancellationToken ct)
    {
        var item = await _uow.Repository<EmailDomain>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"EmailDomain {req.Id} not found.");
        item.Update(req.Dominio);
        await _uow.Repository<EmailDomain>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
public sealed class UpdateEmailDomainValidator : AbstractValidator<UpdateEmailDomain>
{
    public UpdateEmailDomainValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Dominio).NotEmpty().Length(4, 100).Matches(@"^(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,63}$");
    }
}

public sealed record DeleteEmailDomain(int Id) : IRequest;
public sealed class DeleteEmailDomainHandler : IRequestHandler<DeleteEmailDomain>
{
    private readonly IUnitOfWork _uow;
    public DeleteEmailDomainHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteEmailDomain req, CancellationToken ct)
    {
        var item = await _uow.Repository<EmailDomain>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"EmailDomain {req.Id} not found.");
        await _uow.Repository<EmailDomain>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}