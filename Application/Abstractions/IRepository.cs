using Domain.Common;

namespace Application.Abstractions;

public interface IRepository<T> where T : BaseEntity<int>
{
    Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(T entity, CancellationToken ct = default);
    Task UpdateAsync(T entity, CancellationToken ct = default);
    Task RemoveAsync(T entity, CancellationToken ct = default);
}
