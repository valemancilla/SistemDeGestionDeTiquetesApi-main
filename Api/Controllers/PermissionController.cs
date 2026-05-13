using Api.Dtos.Auth;
using Application.Abstractions;
using Domain.Entities.Auth;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Permission")]
[Route("api/permissions")]
public sealed class PermissionController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PermissionController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PermissionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PermissionDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<Permission>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<PermissionDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PermissionDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Permission>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<PermissionDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(PermissionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PermissionDto>> Create([FromBody] CreatePermissionRequest req, CancellationToken ct)
    {
        var item = new Permission(req.Nombre, req.Descripcion);
        await _uow.Repository<Permission>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<PermissionDto>(item));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] CreatePermissionRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<Permission>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Nombre, req.Descripcion);
        await _uow.Repository<Permission>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Permission>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<Permission>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
