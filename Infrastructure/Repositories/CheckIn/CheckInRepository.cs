using Application.Abstractions;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.CheckIn;

public sealed class CheckInRepository : ICheckInRepository
{
    private readonly AppDbContext _ctx;
    public CheckInRepository(AppDbContext ctx) => _ctx = ctx;

    public Task<Domain.Entities.CheckIn.CheckIn?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _ctx.CheckIns.AsTracking().Include(c => c.EstadoCheckin).FirstOrDefaultAsync(c => c.Id == id, ct);

    public Task<Domain.Entities.CheckIn.CheckIn?> GetByTiqueteAsync(int tiqueteId, CancellationToken ct = default) =>
        _ctx.CheckIns.AsTracking().FirstOrDefaultAsync(c => c.TiqueteId == tiqueteId, ct);

    public async Task<IReadOnlyList<Domain.Entities.CheckIn.CheckIn>> GetAllAsync(CancellationToken ct = default) =>
        await _ctx.CheckIns.AsNoTracking().Include(c => c.EstadoCheckin).ToListAsync(ct);

    public Task AddAsync(Domain.Entities.CheckIn.CheckIn c, CancellationToken ct = default) { _ctx.CheckIns.Add(c); return Task.CompletedTask; }
    public Task UpdateAsync(Domain.Entities.CheckIn.CheckIn c, CancellationToken ct = default) { _ctx.CheckIns.Update(c); return Task.CompletedTask; }
}
