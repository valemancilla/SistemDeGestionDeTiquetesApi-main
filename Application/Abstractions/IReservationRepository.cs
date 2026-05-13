using Domain.Entities.Reservations;

namespace Application.Abstractions;

public interface IReservationRepository
{
    Task<Reservation?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Reservation?> GetByCodigoAsync(string codigo, CancellationToken ct = default);
    Task<IReadOnlyList<Reservation>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Reservation>> GetByClienteAsync(int clienteId, CancellationToken ct = default);
    Task AddAsync(Reservation reservation, CancellationToken ct = default);
    Task UpdateAsync(Reservation reservation, CancellationToken ct = default);
    Task RemoveAsync(Reservation reservation, CancellationToken ct = default);
    Task<bool> ExistsCodigoAsync(string codigo, CancellationToken ct = default);
}
