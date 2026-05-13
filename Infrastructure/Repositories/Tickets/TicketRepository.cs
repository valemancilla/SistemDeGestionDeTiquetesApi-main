using Application.Abstractions;
using Domain.Entities.Tickets;
using Domain.ValueObjects.Tickets;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Tickets;

public sealed class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _ctx;
    public TicketRepository(AppDbContext ctx) => _ctx = ctx;

    public Task<Ticket?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _ctx.Tickets.AsTracking().Include(t => t.EstadoTiquete).FirstOrDefaultAsync(t => t.Id == id, ct);

    public Task<Ticket?> GetByCodigoAsync(string codigo, CancellationToken ct = default)
    {
        var code = TicketCode.Create(codigo);
        return _ctx.Tickets.AsTracking().FirstOrDefaultAsync(t => t.CodigoTiquete == code, ct);
    }

    public async Task<IReadOnlyList<Ticket>> GetAllAsync(CancellationToken ct = default) =>
        await _ctx.Tickets.AsNoTracking().Include(t => t.EstadoTiquete).ToListAsync(ct);

    public async Task<IReadOnlyList<Ticket>> GetByReservaPasajeroAsync(int reservaPasajeroId, CancellationToken ct = default) =>
        await _ctx.Tickets.AsNoTracking().Where(t => t.ReservaPasajeroId == reservaPasajeroId).ToListAsync(ct);

    public Task AddAsync(Ticket t, CancellationToken ct = default) { _ctx.Tickets.Add(t); return Task.CompletedTask; }
    public Task UpdateAsync(Ticket t, CancellationToken ct = default) { _ctx.Tickets.Update(t); return Task.CompletedTask; }
    public Task RemoveAsync(Ticket t, CancellationToken ct = default) { _ctx.Tickets.Remove(t); return Task.CompletedTask; }
    public Task<bool> ExistsCodigoAsync(string codigo, CancellationToken ct = default)
    {
        var code = TicketCode.Create(codigo);
        return _ctx.Tickets.AnyAsync(t => t.CodigoTiquete == code, ct);
    }
}
