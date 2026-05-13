using Application.Abstractions;
using Domain.Entities.Auth;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Auth;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _ctx;
    public UserRepository(AppDbContext ctx) => _ctx = ctx;

    public Task<AppUser?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _ctx.Users.AsTracking().Include(u => u.Rol).FirstOrDefaultAsync(u => u.Id == id, ct);

    public Task<AppUser?> GetByUsernameAsync(string username, CancellationToken ct = default)
    {
        var login = global::Domain.ValueObjects.Auth.Username.Create(username);
        return _ctx.Users.AsTracking().FirstOrDefaultAsync(u => u.Username == login, ct);
    }

    public async Task<IReadOnlyList<AppUser>> GetAllAsync(CancellationToken ct = default) =>
        await _ctx.Users.AsNoTracking().Include(u => u.Rol).OrderBy(u => u.Username).ToListAsync(ct);

    public Task AddAsync(AppUser u, CancellationToken ct = default) { _ctx.Users.Add(u); return Task.CompletedTask; }
    public Task UpdateAsync(AppUser u, CancellationToken ct = default) { _ctx.Users.Update(u); return Task.CompletedTask; }
    public Task<bool> ExistsUsernameAsync(string username, CancellationToken ct = default)
    {
        var login = global::Domain.ValueObjects.Auth.Username.Create(username);
        return _ctx.Users.AnyAsync(u => u.Username == login, ct);
    }
}
