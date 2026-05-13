using Api.Dtos.Flights;
using Application.Abstractions;
using Domain.Entities.Flights;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("FlightAssignment")]
[Route("api/flight-assignments")]
public sealed class FlightAssignmentController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public FlightAssignmentController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<FlightAssignmentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<FlightAssignmentDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<FlightAssignment>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<FlightAssignmentDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FlightAssignmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FlightAssignmentDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightAssignment>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<FlightAssignmentDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(FlightAssignmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FlightAssignmentDto>> Create([FromBody] CreateFlightAssignmentRequest req, CancellationToken ct)
    {
        var item = new FlightAssignment(req.VueloId, req.PersonalId, req.RolVueloId);
        await _uow.Repository<FlightAssignment>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<FlightAssignmentDto>((await _uow.Repository<FlightAssignment>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateFlightAssignmentRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightAssignment>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.RolVueloId);
        await _uow.Repository<FlightAssignment>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightAssignment>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<FlightAssignment>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
