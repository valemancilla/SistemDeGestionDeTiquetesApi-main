using Application.Abstractions;
using Domain.Entities.Routes;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Routes;

public sealed class RouteRepository : IRouteRepository
{
    private readonly AppDbContext _ctx;

    public RouteRepository(AppDbContext ctx) => _ctx = ctx;

    public Task<FlightRoute?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _ctx.Routes.AsTracking()
            .Include(r => r.AeropuertoOrigen).ThenInclude(a => a!.Ciudad)
            .Include(r => r.AeropuertoDestino).ThenInclude(a => a!.Ciudad)
            .FirstOrDefaultAsync(r => r.Id == id, ct);

    public Task<FlightRoute?> GetByOrigenDestinoAsync(int origenId, int destinoId, CancellationToken ct = default) =>
        _ctx.Routes.AsNoTracking()
            .FirstOrDefaultAsync(r => r.AeropuertoOrigenId == origenId && r.AeropuertoDestinoId == destinoId, ct);

    public async Task<IReadOnlyList<FlightRoute>> GetAllAsync(CancellationToken ct = default) =>
        await _ctx.Routes.AsNoTracking()
            .Include(r => r.AeropuertoOrigen)
            .Include(r => r.AeropuertoDestino)
            .ToListAsync(ct);

    public async Task<IReadOnlyList<FlightRoute>> GetByOrigenAsync(int aeropuertoOrigenId, CancellationToken ct = default) =>
        await _ctx.Routes.AsNoTracking()
            .Where(r => r.AeropuertoOrigenId == aeropuertoOrigenId)
            .Include(r => r.AeropuertoDestino)
            .ToListAsync(ct);

    public Task AddAsync(FlightRoute route, CancellationToken ct = default)
    {
        _ctx.Routes.Add(route);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(int origenId, int destinoId, CancellationToken ct = default) =>
        _ctx.Routes.AnyAsync(r => r.AeropuertoOrigenId == origenId && r.AeropuertoDestinoId == destinoId, ct);
}
