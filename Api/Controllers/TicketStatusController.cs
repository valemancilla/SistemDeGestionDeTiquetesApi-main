using Api.Dtos.Common;
using Application.Abstractions;
using Domain.Entities.Tickets;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("TicketStatus")]
[Route("api/ticket-statuses")]
public sealed class TicketStatusController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public TicketStatusController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<LookupDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<TicketStatus>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<LookupDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LookupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LookupDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<TicketStatus>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<LookupDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(LookupDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LookupDto>> Create([FromBody] CreateLookupRequest req, CancellationToken ct)
    {
        var item = new TicketStatus(req.Nombre);
        await _uow.Repository<TicketStatus>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<LookupDto>(item));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] CreateLookupRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<TicketStatus>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Nombre);
        await _uow.Repository<TicketStatus>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<TicketStatus>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<TicketStatus>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
