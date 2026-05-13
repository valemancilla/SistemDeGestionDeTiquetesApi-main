using Api.Dtos.Routes;
using Application.Abstractions;
using Domain.Entities.Routes;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Routes")]
[Route("api/routes")]
public sealed class RoutesController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public RoutesController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<RouteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RouteDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Routes.GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<RouteDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RouteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RouteDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Routes.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<RouteDto>(item));
    }

    [HttpGet("by-origen/{aeropuertoId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<RouteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RouteDto>>> GetByOrigen(int aeropuertoId, CancellationToken ct)
    {
        var items = await _uow.Routes.GetByOrigenAsync(aeropuertoId, ct);
        return Ok(_mapper.Map<IReadOnlyList<RouteDto>>(items));
    }

    [HttpPost]
    [ProducesResponseType(typeof(RouteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<RouteDto>> Create([FromBody] CreateRouteRequest req, CancellationToken ct)
    {
        if (await _uow.Routes.ExistsAsync(req.AeropuertoOrigenId, req.AeropuertoDestinoId, ct))
            return Conflict();

        var item = new FlightRoute(req.AeropuertoOrigenId, req.AeropuertoDestinoId, req.DistanciaKm, req.DuracionEstimadaMin);
        await _uow.Routes.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        var created = await _uow.Routes.GetByIdAsync(item.Id, ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<RouteDto>(created!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRouteRequest req, CancellationToken ct)
    {
        var item = await _uow.Routes.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.DistanciaKm, req.DuracionEstimadaMin);
        await _uow.Repository<FlightRoute>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Routes.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<FlightRoute>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
