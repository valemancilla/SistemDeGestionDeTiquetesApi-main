using Application.Abstractions;
using Domain.Entities.Clients;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Clients;

public sealed record GetClients : IRequest<IReadOnlyList<Client>>;
public sealed class GetClientsHandler : IRequestHandler<GetClients, IReadOnlyList<Client>>
{
    private readonly IUnitOfWork _uow;
    public GetClientsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<Client>> Handle(GetClients _, CancellationToken ct)
        => _uow.Clients.GetAllAsync(ct);
}

public sealed record GetClientById(int Id) : IRequest<Client?>;
public sealed class GetClientByIdHandler : IRequestHandler<GetClientById, Client?>
{
    private readonly IUnitOfWork _uow;
    public GetClientByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<Client?> Handle(GetClientById req, CancellationToken ct)
        => _uow.Clients.GetByIdAsync(req.Id, ct);
}

public sealed record CreateClient(int PersonaId) : IRequest<int>;
public sealed class CreateClientHandler : IRequestHandler<CreateClient, int>
{
    private readonly IUnitOfWork _uow;
    public CreateClientHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateClient req, CancellationToken ct)
    {
        var item = new Client(req.PersonaId);
        await _uow.Clients.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateClientValidator : AbstractValidator<CreateClient>
{
    public CreateClientValidator() => RuleFor(x => x.PersonaId).GreaterThan(0);
}

public sealed record DeleteClient(int Id) : IRequest;
public sealed class DeleteClientHandler : IRequestHandler<DeleteClient>
{
    private readonly IUnitOfWork _uow;
    public DeleteClientHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteClient req, CancellationToken ct)
    {
        var item = await _uow.Clients.GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Client {req.Id} not found.");
        await _uow.Repository<Client>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}