using Domain.Entities.CheckIn;

namespace Application.Abstractions;

public interface ICheckInRepository
{
    Task<CheckIn?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<CheckIn?> GetByTiqueteAsync(int tiqueteId, CancellationToken ct = default);
    Task<IReadOnlyList<CheckIn>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(CheckIn checkIn, CancellationToken ct = default);
    Task UpdateAsync(CheckIn checkIn, CancellationToken ct = default);
}
