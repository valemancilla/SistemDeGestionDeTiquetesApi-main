using Api.Dtos.Auth;
using Application.Abstractions;
using Domain.Entities.Auth;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("SystemRole")]
[Route("api/system-roles")]
public sealed class SystemRoleController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public SystemRoleController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<SystemRoleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SystemRoleDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<SystemRole>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<SystemRoleDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SystemRoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SystemRoleDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<SystemRole>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<SystemRoleDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(SystemRoleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SystemRoleDto>> Create([FromBody] CreateSystemRoleRequest req, CancellationToken ct)
    {
        var item = new SystemRole(req.Nombre, req.Descripcion);
        await _uow.Repository<SystemRole>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<SystemRoleDto>(item));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] CreateSystemRoleRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<SystemRole>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Nombre, req.Descripcion);
        await _uow.Repository<SystemRole>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<SystemRole>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<SystemRole>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
