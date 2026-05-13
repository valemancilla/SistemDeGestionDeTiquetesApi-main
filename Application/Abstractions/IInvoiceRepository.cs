using Domain.Entities.Invoices;

namespace Application.Abstractions;

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Invoice?> GetByReservaAsync(int reservaId, CancellationToken ct = default);
    Task<Invoice?> GetByNumeroAsync(string numero, CancellationToken ct = default);
    Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<InvoiceItem>> GetItemsByFacturaAsync(int facturaId, CancellationToken ct = default);
    Task AddAsync(Invoice invoice, CancellationToken ct = default);
    Task UpdateAsync(Invoice invoice, CancellationToken ct = default);
    Task RemoveAsync(Invoice invoice, CancellationToken ct = default);
    Task<bool> ExistsNumeroFacturaAsync(string numero, CancellationToken ct = default);
}
