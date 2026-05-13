using Api.Dtos.Staff;
using Application.Abstractions;
using Domain.Entities.Staff;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("StaffAvailability")]
[Route("api/staff-availability")]
public sealed class StaffAvailabilityController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public StaffAvailabilityController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<StaffAvailabilityDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<StaffAvailabilityDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<StaffAvailability>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<StaffAvailabilityDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(StaffAvailabilityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StaffAvailabilityDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<StaffAvailability>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<StaffAvailabilityDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(StaffAvailabilityDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StaffAvailabilityDto>> Create([FromBody] CreateStaffAvailabilityRequest req, CancellationToken ct)
    {
        var item = new StaffAvailability(req.PersonalId, req.EstadoDisponibilidadId,
            req.FechaInicio, req.FechaFin, req.Observacion);
        await _uow.Repository<StaffAvailability>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<StaffAvailabilityDto>((await _uow.Repository<StaffAvailability>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStaffAvailabilityRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<StaffAvailability>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.EstadoDisponibilidadId, req.FechaInicio, req.FechaFin, req.Observacion);
        await _uow.Repository<StaffAvailability>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<StaffAvailability>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<StaffAvailability>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
