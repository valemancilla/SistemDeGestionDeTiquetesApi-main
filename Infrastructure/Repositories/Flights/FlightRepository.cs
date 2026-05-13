using Application.Abstractions;
using Domain.Entities.Flights;
using Domain.ValueObjects.Aviation;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Flights;

public sealed class FlightRepository : IFlightRepository
{
    private readonly AppDbContext _ctx;

    public FlightRepository(AppDbContext ctx) => _ctx = ctx;

    public Task<Flight?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _ctx.Flights.AsTracking()
            .Include(f => f.Aerolinea)
            .Include(f => f.Ruta).ThenInclude(r => r!.AeropuertoOrigen)
            .Include(f => f.Ruta).ThenInclude(r => r!.AeropuertoDestino)
            .Include(f => f.EstadoVuelo)
            .FirstOrDefaultAsync(f => f.Id == id, ct);

    public Task<Flight?> GetByCodigoAsync(string codigo, CancellationToken ct = default)
    {
        var code = FlightCode.Create(codigo);
        return _ctx.Flights.AsTracking().FirstOrDefaultAsync(f => f.CodigoVuelo == code, ct);
    }

    public async Task<IReadOnlyList<Flight>> GetAllAsync(CancellationToken ct = default) =>
        await _ctx.Flights.AsNoTracking()
            .Include(f => f.Aerolinea)
            .Include(f => f.EstadoVuelo)
            .OrderBy(f => f.FechaSalida)
            .ToListAsync(ct);

    public async Task<IReadOnlyList<Flight>> GetByRutaAsync(int rutaId, CancellationToken ct = default) =>
        await _ctx.Flights.AsNoTracking()
            .Where(f => f.RutaId == rutaId)
            .OrderBy(f => f.FechaSalida)
            .ToListAsync(ct);

    public async Task<IReadOnlyList<Flight>> GetByFechaAsync(DateTime fecha, CancellationToken ct = default) =>
        await _ctx.Flights.AsNoTracking()
            .Where(f => f.FechaSalida.Date == fecha.Date)
            .OrderBy(f => f.FechaSalida)
            .ToListAsync(ct);

    public Task AddAsync(Flight flight, CancellationToken ct = default)
    {
        _ctx.Flights.Add(flight);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Flight flight, CancellationToken ct = default)
    {
        _ctx.Flights.Update(flight);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsCodigoAsync(string codigo, CancellationToken ct = default)
    {
        var code = FlightCode.Create(codigo);
        return _ctx.Flights.AnyAsync(f => f.CodigoVuelo == code, ct);
    }
}
