using Api.Dtos.Maintenance;
using Application.Abstractions;
using Domain.Entities.Maintenance;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("MaintenanceType")]
[Route("api/maintenance-types")]
public sealed class MaintenanceTypeController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public MaintenanceTypeController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MaintenanceTypeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<MaintenanceTypeDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<MaintenanceType>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<MaintenanceTypeDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MaintenanceTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaintenanceTypeDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<MaintenanceType>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<MaintenanceTypeDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(MaintenanceTypeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MaintenanceTypeDto>> Create([FromBody] CreateMaintenanceTypeRequest req, CancellationToken ct)
    {
        var item = new MaintenanceType(req.Nombre);
        await _uow.Repository<MaintenanceType>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<MaintenanceTypeDto>(item));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMaintenanceTypeRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<MaintenanceType>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Nombre);
        await _uow.Repository<MaintenanceType>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<MaintenanceType>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<MaintenanceType>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
