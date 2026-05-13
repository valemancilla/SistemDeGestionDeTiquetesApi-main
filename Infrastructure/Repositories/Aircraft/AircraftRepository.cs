using Application.Abstractions;
using Domain.ValueObjects.Aviation;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Aircraft;

public sealed class AircraftRepository : IAircraftRepository
{
    private readonly AppDbContext _ctx;

    public AircraftRepository(AppDbContext ctx) => _ctx = ctx;

    public Task<Domain.Entities.Aircraft.Aircraft?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _ctx.Aircraft.AsTracking()
            .Include(a => a.Modelo).ThenInclude(m => m!.Fabricante)
            .Include(a => a.Aerolinea)
            .FirstOrDefaultAsync(a => a.Id == id, ct);

    public Task<Domain.Entities.Aircraft.Aircraft?> GetByMatriculaAsync(string matricula, CancellationToken ct = default)
    {
        var reg = AircraftRegistration.Create(matricula);
        return _ctx.Aircraft.AsTracking().FirstOrDefaultAsync(a => a.Matricula == reg, ct);
    }

    public async Task<IReadOnlyList<Domain.Entities.Aircraft.Aircraft>> GetAllAsync(CancellationToken ct = default) =>
        await _ctx.Aircraft.AsNoTracking()
            .Include(a => a.Modelo).ThenInclude(m => m!.Fabricante)
            .Include(a => a.Aerolinea)
            .OrderBy(a => a.Matricula).ToListAsync(ct);

    public async Task<IReadOnlyList<Domain.Entities.Aircraft.Aircraft>> GetByAerolineaAsync(int aerolineaId, CancellationToken ct = default) =>
        await _ctx.Aircraft.AsNoTracking().Where(a => a.AerolineaId == aerolineaId).ToListAsync(ct);

    public Task AddAsync(Domain.Entities.Aircraft.Aircraft aircraft, CancellationToken ct = default)
    {
        _ctx.Aircraft.Add(aircraft);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Domain.Entities.Aircraft.Aircraft aircraft, CancellationToken ct = default)
    {
        _ctx.Aircraft.Update(aircraft);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsMatriculaAsync(string matricula, CancellationToken ct = default)
    {
        var reg = AircraftRegistration.Create(matricula);
        return _ctx.Aircraft.AnyAsync(a => a.Matricula == reg, ct);
    }
}
