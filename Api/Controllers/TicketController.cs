using Api.Dtos.Tickets;
using Application.Abstractions;
using Domain.Entities.Tickets;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Ticket")]
[Route("api/tickets")]
public sealed class TicketController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public TicketController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TicketDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TicketDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Tickets.GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<TicketDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Tickets.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<TicketDto>(item));
    }

    [HttpGet("by-codigo/{codigo}")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> GetByCodigo(string codigo, CancellationToken ct)
    {
        var item = await _uow.Tickets.GetByCodigoAsync(codigo, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<TicketDto>(item));
    }

    [HttpGet("by-reserva-pasajero/{reservaPasajeroId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<TicketDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TicketDto>>> GetByReservaPasajero(int reservaPasajeroId, CancellationToken ct)
    {
        var items = await _uow.Tickets.GetByReservaPasajeroAsync(reservaPasajeroId, ct);
        return Ok(_mapper.Map<IReadOnlyList<TicketDto>>(items));
    }

    [HttpPost]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<TicketDto>> Create([FromBody] CreateTicketRequest req, CancellationToken ct)
    {
        if (await _uow.Tickets.ExistsCodigoAsync(req.CodigoTiquete, ct))
            return Conflict();

        var item = new Ticket(req.ReservaPasajeroId, req.CodigoTiquete, req.FechaEmision, req.EstadoTiqueteId);
        await _uow.Tickets.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        var created = await _uow.Tickets.GetByIdAsync(item.Id, ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<TicketDto>(created!));
    }

    [HttpPut("{id:int}/estado")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEstado(int id, [FromBody] UpdateTicketStatusRequest req, CancellationToken ct)
    {
        var item = await _uow.Tickets.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.CambiarEstado(req.NuevoEstadoId);
        await _uow.Tickets.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Tickets.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Tickets.RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
