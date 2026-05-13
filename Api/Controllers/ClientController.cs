using Api.Dtos.Clients;
using Application.Abstractions;
using Domain.Entities.Clients;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Client")]
[Route("api/clients")]
public sealed class ClientController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ClientController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Clients.GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<ClientDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Clients.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<ClientDto>(item));
    }

    [HttpGet("by-persona/{personaId:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientDto>> GetByPersonaId(int personaId, CancellationToken ct)
    {
        var item = await _uow.Clients.GetByPersonaIdAsync(personaId, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<ClientDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ClientDto>> Create([FromBody] CreateClientRequest req, CancellationToken ct)
    {
        if (await _uow.Clients.ExistsPersonaAsync(req.PersonaId, ct))
            return Conflict();

        var item = new Client(req.PersonaId);
        await _uow.Clients.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<ClientDto>((await _uow.Clients.GetByIdAsync(item.Id, ct))!));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Clients.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<Client>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
