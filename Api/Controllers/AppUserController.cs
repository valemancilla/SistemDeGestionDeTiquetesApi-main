using Api.Dtos.Auth;
using Application.Abstractions;
using Domain.Entities.Auth;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("AppUser")]
[Route("api/users")]
public sealed class AppUserController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public AppUserController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AppUserDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AppUserDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Users.GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<AppUserDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AppUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppUserDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Users.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<AppUserDto>(item));
    }

    [HttpGet("by-username/{username}")]
    [ProducesResponseType(typeof(AppUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppUserDto>> GetByUsername(string username, CancellationToken ct)
    {
        var item = await _uow.Users.GetByUsernameAsync(username, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<AppUserDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(AppUserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AppUserDto>> Create([FromBody] CreateAppUserRequest req, CancellationToken ct)
    {
        if (await _uow.Users.ExistsUsernameAsync(req.Username, ct))
            return Conflict();

        var item = new AppUser(req.Username, req.PasswordHash, req.RolId, req.PersonaId);
        await _uow.Users.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        var created = await _uow.Users.GetByIdAsync(item.Id, ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<AppUserDto>(created!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAppUserRequest req, CancellationToken ct)
    {
        var item = await _uow.Users.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.RolId, req.Activo);
        await _uow.Users.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpPut("{id:int}/password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordRequest req, CancellationToken ct)
    {
        var item = await _uow.Users.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.CambiarPassword(req.PasswordHash);
        await _uow.Users.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
