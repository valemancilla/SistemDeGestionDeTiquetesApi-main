using Api.Dtos.Reservations;
using Application.Abstractions;
using Domain.Entities.Reservations;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Reservation")]
[Route("api/reservations")]
public sealed class ReservationController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ReservationController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ReservationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ReservationDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Reservations.GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<ReservationDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReservationDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Reservations.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<ReservationDto>(item));
    }

    [HttpGet("by-codigo/{codigo}")]
    [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReservationDto>> GetByCodigo(string codigo, CancellationToken ct)
    {
        var item = await _uow.Reservations.GetByCodigoAsync(codigo, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<ReservationDto>(item));
    }

    [HttpGet("by-cliente/{clienteId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<ReservationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ReservationDto>>> GetByCliente(int clienteId, CancellationToken ct)
    {
        var items = await _uow.Reservations.GetByClienteAsync(clienteId, ct);
        return Ok(_mapper.Map<IReadOnlyList<ReservationDto>>(items));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ReservationDto>> Create([FromBody] CreateReservationRequest req, CancellationToken ct)
    {
        if (await _uow.Reservations.ExistsCodigoAsync(req.CodigoReserva, ct))
            return Conflict();

        var item = new Reservation(req.CodigoReserva, req.ClienteId, req.EstadoReservaId, req.ValorTotal, req.VenceEn);
        await _uow.Reservations.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        var created = await _uow.Reservations.GetByIdAsync(item.Id, ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<ReservationDto>(created!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateReservationRequest req, CancellationToken ct)
    {
        var item = await _uow.Reservations.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.ValorTotal, req.VenceEn);
        await _uow.Reservations.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpPut("{id:int}/estado")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEstado(int id, [FromBody] UpdateReservationStatusRequest req, CancellationToken ct)
    {
        var item = await _uow.Reservations.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.CambiarEstado(req.NuevoEstadoId);
        await _uow.Reservations.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Reservations.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Reservations.RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
