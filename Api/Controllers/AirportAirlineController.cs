using Api.Dtos.Airport;
using Application.Abstractions;
using Domain.Entities.Airports;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("AirportAirline")]
[Route("api/airport-airlines")]
public sealed class AirportAirlineController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public AirportAirlineController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AirportAirlineDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AirportAirlineDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<AirportAirline>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<AirportAirlineDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AirportAirlineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AirportAirlineDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<AirportAirline>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<AirportAirlineDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(AirportAirlineDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AirportAirlineDto>> Create([FromBody] CreateAirportAirlineRequest req, CancellationToken ct)
    {
        var item = new AirportAirline(req.AeropuertoId, req.AerolineaId, req.FechaInicio, req.Terminal, req.FechaFin);
        await _uow.Repository<AirportAirline>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<AirportAirlineDto>((await _uow.Repository<AirportAirline>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAirportAirlineRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<AirportAirline>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Terminal, req.FechaFin, req.Activa);
        await _uow.Repository<AirportAirline>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<AirportAirline>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<AirportAirline>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
