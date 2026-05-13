using Api.Dtos.Flights;
using Application.Abstractions;
using Domain.Entities.Flights;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("FlightStatusTransition")]
[Route("api/flight-status-transitions")]
public sealed class FlightStatusTransitionController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public FlightStatusTransitionController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<FlightStatusTransitionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<FlightStatusTransitionDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<FlightStatusTransition>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<FlightStatusTransitionDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FlightStatusTransitionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FlightStatusTransitionDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightStatusTransition>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<FlightStatusTransitionDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(FlightStatusTransitionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FlightStatusTransitionDto>> Create([FromBody] CreateFlightStatusTransitionRequest req, CancellationToken ct)
    {
        var item = new FlightStatusTransition(req.EstadoOrigenId, req.EstadoDestinoId);
        await _uow.Repository<FlightStatusTransition>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<FlightStatusTransitionDto>((await _uow.Repository<FlightStatusTransition>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightStatusTransition>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<FlightStatusTransition>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
