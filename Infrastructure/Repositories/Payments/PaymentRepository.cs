using Application.Abstractions;
using Domain.Entities.Payments;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Payments;

public sealed class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _ctx;
    public PaymentRepository(AppDbContext ctx) => _ctx = ctx;

    public Task<Payment?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _ctx.Payments.AsTracking().Include(p => p.EstadoPago).Include(p => p.MetodoPago)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken ct = default) =>
        await _ctx.Payments.AsNoTracking().Include(p => p.EstadoPago).OrderByDescending(p => p.FechaPago).ToListAsync(ct);

    public async Task<IReadOnlyList<Payment>> GetByReservaAsync(int reservaId, CancellationToken ct = default) =>
        await _ctx.Payments.AsNoTracking().Where(p => p.ReservaId == reservaId).ToListAsync(ct);

    public Task AddAsync(Payment p, CancellationToken ct = default) { _ctx.Payments.Add(p); return Task.CompletedTask; }
    public Task UpdateAsync(Payment p, CancellationToken ct = default) { _ctx.Payments.Update(p); return Task.CompletedTask; }
    public Task RemoveAsync(Payment p, CancellationToken ct = default) { _ctx.Payments.Remove(p); return Task.CompletedTask; }
}
