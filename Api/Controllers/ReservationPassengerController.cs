using Api.Dtos.Reservations;
using Application.Abstractions;
using Domain.Entities.Reservations;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("ReservationPassenger")]
[Route("api/reservation-passengers")]
public sealed class ReservationPassengerController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ReservationPassengerController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ReservationPassengerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ReservationPassengerDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<ReservationPassenger>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<ReservationPassengerDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ReservationPassengerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReservationPassengerDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<ReservationPassenger>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<ReservationPassengerDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReservationPassengerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReservationPassengerDto>> Create([FromBody] CreateReservationPassengerRequest req, CancellationToken ct)
    {
        var item = new ReservationPassenger(req.ReservaVueloId, req.PasajeroId);
        await _uow.Repository<ReservationPassenger>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<ReservationPassengerDto>((await _uow.Repository<ReservationPassenger>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<ReservationPassenger>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<ReservationPassenger>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
