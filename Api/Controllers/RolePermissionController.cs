using Api.Dtos.Auth;
using Application.Abstractions;
using Domain.Entities.Auth;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("RolePermission")]
[Route("api/role-permissions")]
public sealed class RolePermissionController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public RolePermissionController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<RolePermissionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RolePermissionDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<RolePermission>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<RolePermissionDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RolePermissionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RolePermissionDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<RolePermission>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<RolePermissionDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(RolePermissionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RolePermissionDto>> Create([FromBody] CreateRolePermissionRequest req, CancellationToken ct)
    {
        var item = new RolePermission(req.RolId, req.PermisoId);
        await _uow.Repository<RolePermission>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<RolePermissionDto>((await _uow.Repository<RolePermission>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<RolePermission>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<RolePermission>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
