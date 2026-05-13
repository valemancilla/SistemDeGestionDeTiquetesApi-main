using Domain.Common;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public sealed class EfRepositoryWithIncludes<T> : EfRepository<T> where T : BaseEntity<int>
{
    private readonly Func<IQueryable<T>, IQueryable<T>> _includes;

    public EfRepositoryWithIncludes(AppDbContext ctx, Func<IQueryable<T>, IQueryable<T>> includes)
        : base(ctx) => _includes = includes;

    protected override IQueryable<T> WithIncludes(IQueryable<T> q) => _includes(q);
}
