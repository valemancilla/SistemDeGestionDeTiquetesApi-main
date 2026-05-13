using Domain.Entities.Payments;

namespace Application.Abstractions;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Payment>> GetByReservaAsync(int reservaId, CancellationToken ct = default);
    Task AddAsync(Payment payment, CancellationToken ct = default);
    Task UpdateAsync(Payment payment, CancellationToken ct = default);
    Task RemoveAsync(Payment payment, CancellationToken ct = default);
}
