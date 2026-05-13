using Api.Dtos.Maintenance;
using Application.Abstractions;
using Domain.Entities.Maintenance;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("AircraftMaintenance")]
[Route("api/aircraft-maintenance")]
public sealed class AircraftMaintenanceController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public AircraftMaintenanceController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AircraftMaintenanceDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AircraftMaintenanceDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<AircraftMaintenance>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<AircraftMaintenanceDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AircraftMaintenanceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AircraftMaintenanceDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<AircraftMaintenance>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<AircraftMaintenanceDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(AircraftMaintenanceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AircraftMaintenanceDto>> Create([FromBody] CreateAircraftMaintenanceRequest req, CancellationToken ct)
    {
        var item = new AircraftMaintenance(req.AeronaveId, req.TipoMantenimientoId, req.FechaInicio, req.Descripcion);
        await _uow.Repository<AircraftMaintenance>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<AircraftMaintenanceDto>((await _uow.Repository<AircraftMaintenance>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAircraftMaintenanceRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<AircraftMaintenance>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.TipoMantenimientoId, req.FechaInicio, req.FechaFin, req.Descripcion);
        await _uow.Repository<AircraftMaintenance>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<AircraftMaintenance>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<AircraftMaintenance>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
