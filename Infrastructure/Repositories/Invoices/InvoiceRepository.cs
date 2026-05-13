using Application.Abstractions;
using Domain.Entities.Invoices;
using Domain.ValueObjects.Invoices;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Invoices;

public sealed class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _ctx;
    public InvoiceRepository(AppDbContext ctx) => _ctx = ctx;

    public Task<Invoice?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _ctx.Invoices.AsTracking().FirstOrDefaultAsync(i => i.Id == id, ct);

    public Task<Invoice?> GetByReservaAsync(int reservaId, CancellationToken ct = default) =>
        _ctx.Invoices.AsTracking().FirstOrDefaultAsync(i => i.ReservaId == reservaId, ct);

    public Task<Invoice?> GetByNumeroAsync(string numero, CancellationToken ct = default)
    {
        var n = InvoiceNumber.Create(numero);
        return _ctx.Invoices.AsTracking().FirstOrDefaultAsync(i => i.NumeroFactura == n, ct);
    }

    public async Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken ct = default) =>
        await _ctx.Invoices.AsNoTracking().OrderByDescending(i => i.FechaEmision).ToListAsync(ct);

    public async Task<IReadOnlyList<InvoiceItem>> GetItemsByFacturaAsync(int facturaId, CancellationToken ct = default) =>
        await _ctx.InvoiceItems.AsNoTracking().Include(i => i.TipoItem).Where(i => i.FacturaId == facturaId).ToListAsync(ct);

    public Task AddAsync(Invoice i, CancellationToken ct = default) { _ctx.Invoices.Add(i); return Task.CompletedTask; }
    public Task UpdateAsync(Invoice i, CancellationToken ct = default) { _ctx.Invoices.Update(i); return Task.CompletedTask; }
    public Task RemoveAsync(Invoice i, CancellationToken ct = default) { _ctx.Invoices.Remove(i); return Task.CompletedTask; }
    public Task<bool> ExistsNumeroFacturaAsync(string numero, CancellationToken ct = default)
    {
        var n = InvoiceNumber.Create(numero);
        return _ctx.Invoices.AnyAsync(i => i.NumeroFactura == n, ct);
    }
}
