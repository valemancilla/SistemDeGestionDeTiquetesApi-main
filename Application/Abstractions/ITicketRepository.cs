using Domain.Entities.Tickets;

namespace Application.Abstractions;

public interface ITicketRepository
{
    Task<Ticket?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Ticket?> GetByCodigoAsync(string codigo, CancellationToken ct = default);
    Task<IReadOnlyList<Ticket>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Ticket>> GetByReservaPasajeroAsync(int reservaPasajeroId, CancellationToken ct = default);
    Task AddAsync(Ticket ticket, CancellationToken ct = default);
    Task UpdateAsync(Ticket ticket, CancellationToken ct = default);
    Task RemoveAsync(Ticket ticket, CancellationToken ct = default);
    Task<bool> ExistsCodigoAsync(string codigo, CancellationToken ct = default);
}
