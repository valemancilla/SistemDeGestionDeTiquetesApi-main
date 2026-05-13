using Api.Dtos.Flights;
using Application.Abstractions;
using Domain.Entities.Flights;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("FlightSeat")]
[Route("api/flight-seats")]
public sealed class FlightSeatController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public FlightSeatController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<FlightSeatDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<FlightSeatDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<FlightSeat>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<FlightSeatDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FlightSeatDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FlightSeatDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightSeat>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<FlightSeatDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(FlightSeatDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FlightSeatDto>> Create([FromBody] CreateFlightSeatRequest req, CancellationToken ct)
    {
        var item = new FlightSeat(req.VueloId, req.CodigoAsiento, req.TipoCabinaId, req.TipoUbicacionId);
        await _uow.Repository<FlightSeat>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<FlightSeatDto>((await _uow.Repository<FlightSeat>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateFlightSeatRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightSeat>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        if (req.EstaOcupado) item.Ocupar(); else item.Liberar();
        await _uow.Repository<FlightSeat>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightSeat>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<FlightSeat>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
