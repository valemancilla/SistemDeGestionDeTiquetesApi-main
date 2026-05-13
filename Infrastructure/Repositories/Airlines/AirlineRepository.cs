using Application.Abstractions;
using Domain.Entities.Airlines;
using Domain.ValueObjects.Aviation;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Airlines;

public sealed class AirlineRepository : IAirlineRepository
{
    private readonly AppDbContext _ctx;

    public AirlineRepository(AppDbContext ctx) => _ctx = ctx;

    public Task<Airline?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _ctx.Airlines.AsTracking().Include(a => a.PaisOrigen).FirstOrDefaultAsync(a => a.Id == id, ct);

    public Task<Airline?> GetByCodigoIataAsync(string codigoIata, CancellationToken ct = default)
    {
        var code = IataCode.Create(codigoIata);
        return _ctx.Airlines.AsTracking().FirstOrDefaultAsync(a => a.CodigoIata == code, ct);
    }

    public async Task<IReadOnlyList<Airline>> GetAllAsync(CancellationToken ct = default) =>
        await _ctx.Airlines.AsNoTracking().Include(a => a.PaisOrigen).OrderBy(a => a.Nombre).ToListAsync(ct);

    public async Task<IReadOnlyList<Airline>> GetActivasAsync(CancellationToken ct = default) =>
        await _ctx.Airlines.AsNoTracking().Where(a => a.Activa).OrderBy(a => a.Nombre).ToListAsync(ct);

    public Task AddAsync(Airline airline, CancellationToken ct = default)
    {
        _ctx.Airlines.Add(airline);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Airline airline, CancellationToken ct = default)
    {
        _ctx.Airlines.Update(airline);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsCodigoIataAsync(string codigoIata, CancellationToken ct = default)
    {
        var code = IataCode.Create(codigoIata);
        return _ctx.Airlines.AnyAsync(a => a.CodigoIata == code, ct);
    }
}
