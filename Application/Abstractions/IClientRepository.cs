using Domain.Entities.Clients;

namespace Application.Abstractions;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Client?> GetByPersonaIdAsync(int personaId, CancellationToken ct = default);
    Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Client client, CancellationToken ct = default);
    Task<bool> ExistsPersonaAsync(int personaId, CancellationToken ct = default);
}
