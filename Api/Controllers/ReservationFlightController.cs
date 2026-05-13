using Api.Dtos.Reservations;
using Application.Abstractions;
using Domain.Entities.Reservations;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("ReservationFlight")]
[Route("api/reservation-flights")]
public sealed class ReservationFlightController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ReservationFlightController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ReservationFlightDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ReservationFlightDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<ReservationFlight>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<ReservationFlightDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ReservationFlightDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReservationFlightDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<ReservationFlight>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<ReservationFlightDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReservationFlightDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReservationFlightDto>> Create([FromBody] CreateReservationFlightRequest req, CancellationToken ct)
    {
        var item = new ReservationFlight(req.ReservaId, req.VueloId, req.ValorParcial);
        await _uow.Repository<ReservationFlight>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<ReservationFlightDto>((await _uow.Repository<ReservationFlight>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateReservationFlightRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<ReservationFlight>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.ValorParcial);
        await _uow.Repository<ReservationFlight>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<ReservationFlight>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<ReservationFlight>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
