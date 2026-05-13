using Domain.Entities.Routes;

namespace Application.Abstractions;

public interface IRouteRepository
{
    Task<FlightRoute?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<FlightRoute?> GetByOrigenDestinoAsync(int origenId, int destinoId, CancellationToken ct = default);
    Task<IReadOnlyList<FlightRoute>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<FlightRoute>> GetByOrigenAsync(int aeropuertoOrigenId, CancellationToken ct = default);
    Task AddAsync(FlightRoute route, CancellationToken ct = default);
    Task<bool> ExistsAsync(int origenId, int destinoId, CancellationToken ct = default);
}
