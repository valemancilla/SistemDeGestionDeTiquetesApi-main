using Api.Dtos.Reservations;
using Application.Abstractions;
using Domain.Entities.Reservations;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("ReservationStatusTransition")]
[Route("api/reservation-status-transitions")]
public sealed class ReservationStatusTransitionController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ReservationStatusTransitionController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ReservationStatusTransitionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ReservationStatusTransitionDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<ReservationStatusTransition>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<ReservationStatusTransitionDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ReservationStatusTransitionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReservationStatusTransitionDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<ReservationStatusTransition>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<ReservationStatusTransitionDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReservationStatusTransitionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReservationStatusTransitionDto>> Create([FromBody] CreateReservationStatusTransitionRequest req, CancellationToken ct)
    {
        var item = new ReservationStatusTransition(req.EstadoOrigenId, req.EstadoDestinoId);
        await _uow.Repository<ReservationStatusTransition>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<ReservationStatusTransitionDto>((await _uow.Repository<ReservationStatusTransition>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<ReservationStatusTransition>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<ReservationStatusTransition>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
