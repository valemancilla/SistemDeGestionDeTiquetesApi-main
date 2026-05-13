using Api.Dtos.Auth;
using Application.Abstractions;
using Domain.Entities.Auth;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Session")]
[Route("api/sessions")]
public sealed class SessionController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public SessionController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<SessionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SessionDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<Session>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<SessionDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SessionDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Session>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<SessionDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(SessionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SessionDto>> Create([FromBody] CreateSessionRequest req, CancellationToken ct)
    {
        var item = new Session(req.UsuarioId, req.IpOrigen);
        await _uow.Repository<Session>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<SessionDto>((await _uow.Repository<Session>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}/cerrar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cerrar(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Session>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Cerrar();
        await _uow.Repository<Session>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
