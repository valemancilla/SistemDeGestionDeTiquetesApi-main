using Domain.Entities.Auth;

namespace Application.Abstractions;

public interface IUserRepository
{
    Task<AppUser?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<AppUser?> GetByUsernameAsync(string username, CancellationToken ct = default);
    Task<IReadOnlyList<AppUser>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(AppUser user, CancellationToken ct = default);
    Task UpdateAsync(AppUser user, CancellationToken ct = default);
    Task<bool> ExistsUsernameAsync(string username, CancellationToken ct = default);
}
