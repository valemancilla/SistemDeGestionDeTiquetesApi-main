using Api.Dtos.Staff;
using Application.Abstractions;
using Domain.Entities.Staff;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Staff")]
[Route("api/staff")]
public sealed class StaffController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public StaffController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<StaffDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<StaffDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<Staff>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<StaffDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(StaffDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StaffDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Staff>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<StaffDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(StaffDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StaffDto>> Create([FromBody] CreateStaffRequest req, CancellationToken ct)
    {
        var item = new Staff(req.PersonaId, req.CargoId, req.FechaIngreso, req.AerolineaId, req.AeropuertoId);
        await _uow.Repository<Staff>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<StaffDto>((await _uow.Repository<Staff>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStaffRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<Staff>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.CargoId, req.AerolineaId, req.AeropuertoId, req.Activo);
        await _uow.Repository<Staff>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Staff>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<Staff>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
