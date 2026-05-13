using Domain.Entities.Airlines;

namespace Application.Abstractions;

public interface IAirlineRepository
{
    Task<Airline?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Airline?> GetByCodigoIataAsync(string codigoIata, CancellationToken ct = default);
    Task<IReadOnlyList<Airline>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Airline>> GetActivasAsync(CancellationToken ct = default);
    Task AddAsync(Airline airline, CancellationToken ct = default);
    Task UpdateAsync(Airline airline, CancellationToken ct = default);
    Task<bool> ExistsCodigoIataAsync(string codigoIata, CancellationToken ct = default);
}
