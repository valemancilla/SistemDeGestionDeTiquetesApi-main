using Api.Dtos.Routes;
using Application.Abstractions;
using Domain.Entities.Routes;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("RouteStop")]
[Route("api/route-stops")]
public sealed class RouteStopController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public RouteStopController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<RouteStopDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RouteStopDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<RouteStop>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<RouteStopDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RouteStopDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RouteStopDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<RouteStop>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<RouteStopDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(RouteStopDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RouteStopDto>> Create([FromBody] CreateRouteStopRequest req, CancellationToken ct)
    {
        var item = new RouteStop(req.RutaId, req.AeropuertoEscalaId, req.Orden, req.DuracionEscalaMin);
        await _uow.Repository<RouteStop>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<RouteStopDto>((await _uow.Repository<RouteStop>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRouteStopRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<RouteStop>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Orden, req.DuracionEscalaMin);
        await _uow.Repository<RouteStop>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<RouteStop>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<RouteStop>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
