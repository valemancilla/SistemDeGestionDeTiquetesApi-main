using Api.Dtos.Passengers;
using Application.Abstractions;
using Domain.Entities.Passengers;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Passenger")]
[Route("api/passengers")]
public sealed class PassengerController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PassengerController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PassengerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PassengerDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<Passenger>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<PassengerDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PassengerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PassengerDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Passenger>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<PassengerDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(PassengerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PassengerDto>> Create([FromBody] CreatePassengerRequest req, CancellationToken ct)
    {
        var item = new Passenger(req.PersonaId, req.TipoPasajeroId);
        await _uow.Repository<Passenger>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<PassengerDto>((await _uow.Repository<Passenger>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePassengerRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<Passenger>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.TipoPasajeroId);
        await _uow.Repository<Passenger>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Passenger>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<Passenger>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
