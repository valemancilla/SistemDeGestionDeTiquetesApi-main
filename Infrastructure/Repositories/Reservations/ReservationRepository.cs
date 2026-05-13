using Application.Abstractions;
using Domain.Entities.Reservations;
using Domain.ValueObjects.Reservations;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Reservations;

public sealed class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _ctx;
    public ReservationRepository(AppDbContext ctx) => _ctx = ctx;

    public Task<Reservation?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _ctx.Reservations.AsTracking().Include(r => r.EstadoReserva).Include(r => r.Cliente)
            .FirstOrDefaultAsync(r => r.Id == id, ct);

    public Task<Reservation?> GetByCodigoAsync(string codigo, CancellationToken ct = default)
    {
        var code = BookingCode.Create(codigo);
        return _ctx.Reservations.AsTracking().FirstOrDefaultAsync(r => r.CodigoReserva == code, ct);
    }

    public async Task<IReadOnlyList<Reservation>> GetAllAsync(CancellationToken ct = default) =>
        await _ctx.Reservations.AsNoTracking().Include(r => r.EstadoReserva).OrderByDescending(r => r.CreatedAt).ToListAsync(ct);

    public async Task<IReadOnlyList<Reservation>> GetByClienteAsync(int clienteId, CancellationToken ct = default) =>
        await _ctx.Reservations.AsNoTracking().Where(r => r.ClienteId == clienteId).OrderByDescending(r => r.CreatedAt).ToListAsync(ct);

    public Task AddAsync(Reservation r, CancellationToken ct = default) { _ctx.Reservations.Add(r); return Task.CompletedTask; }
    public Task UpdateAsync(Reservation r, CancellationToken ct = default) { _ctx.Reservations.Update(r); return Task.CompletedTask; }
    public Task RemoveAsync(Reservation r, CancellationToken ct = default) { _ctx.Reservations.Remove(r); return Task.CompletedTask; }
    public Task<bool> ExistsCodigoAsync(string codigo, CancellationToken ct = default)
    {
        var code = BookingCode.Create(codigo);
        return _ctx.Reservations.AnyAsync(r => r.CodigoReserva == code, ct);
    }
}
