using Application.Abstractions;
using Domain.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EfRepository<T> : IRepository<T> where T : BaseEntity<int>
{
    protected readonly AppDbContext Ctx;

    public EfRepository(AppDbContext ctx) => Ctx = ctx;

    protected virtual IQueryable<T> WithIncludes(IQueryable<T> q) => q;

    public Task<T?> GetByIdAsync(int id, CancellationToken ct = default) =>
        WithIncludes(Ctx.Set<T>().AsTracking()).FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default) =>
        await WithIncludes(Ctx.Set<T>().AsNoTracking()).ToListAsync(ct);

    public Task AddAsync(T entity, CancellationToken ct = default)
    {
        Ctx.Set<T>().Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        Ctx.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(T entity, CancellationToken ct = default)
    {
        Ctx.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }
}
