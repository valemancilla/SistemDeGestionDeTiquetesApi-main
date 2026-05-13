using Application.Abstractions;
using Domain.Entities.Clients;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Clients;

public sealed class ClientRepository : IClientRepository
{
    private readonly AppDbContext _ctx;

    public ClientRepository(AppDbContext ctx) => _ctx = ctx;

    public Task<Client?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _ctx.Clients.AsTracking().Include(c => c.Persona).FirstOrDefaultAsync(c => c.Id == id, ct);

    public Task<Client?> GetByPersonaIdAsync(int personaId, CancellationToken ct = default) =>
        _ctx.Clients.AsTracking().FirstOrDefaultAsync(c => c.PersonaId == personaId, ct);

    public async Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken ct = default) =>
        await _ctx.Clients.AsNoTracking().Include(c => c.Persona).ToListAsync(ct);

    public Task AddAsync(Client client, CancellationToken ct = default)
    {
        _ctx.Clients.Add(client);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsPersonaAsync(int personaId, CancellationToken ct = default) =>
        _ctx.Clients.AnyAsync(c => c.PersonaId == personaId, ct);
}
