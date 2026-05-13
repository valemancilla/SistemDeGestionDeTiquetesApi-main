using Domain.Entities.Aircraft;

namespace Application.Abstractions;

public interface IAircraftRepository
{
    Task<Aircraft?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Aircraft?> GetByMatriculaAsync(string matricula, CancellationToken ct = default);
    Task<IReadOnlyList<Aircraft>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Aircraft>> GetByAerolineaAsync(int aerolineaId, CancellationToken ct = default);
    Task AddAsync(Aircraft aircraft, CancellationToken ct = default);
    Task UpdateAsync(Aircraft aircraft, CancellationToken ct = default);
    Task<bool> ExistsMatriculaAsync(string matricula, CancellationToken ct = default);
}
