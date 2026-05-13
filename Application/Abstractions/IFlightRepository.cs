using Domain.Entities.Flights;

namespace Application.Abstractions;

public interface IFlightRepository
{
    Task<Flight?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Flight?> GetByCodigoAsync(string codigo, CancellationToken ct = default);
    Task<IReadOnlyList<Flight>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Flight>> GetByRutaAsync(int rutaId, CancellationToken ct = default);
    Task<IReadOnlyList<Flight>> GetByFechaAsync(DateTime fecha, CancellationToken ct = default);
    Task AddAsync(Flight flight, CancellationToken ct = default);
    Task UpdateAsync(Flight flight, CancellationToken ct = default);
    Task<bool> ExistsCodigoAsync(string codigo, CancellationToken ct = default);
}
