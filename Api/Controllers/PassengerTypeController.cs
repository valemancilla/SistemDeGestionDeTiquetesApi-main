using Api.Dtos.Fares;
using Application.Abstractions;
using Domain.Entities.Fares;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("PassengerType")]
[Route("api/passenger-types")]
public sealed class PassengerTypeController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PassengerTypeController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PassengerTypeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PassengerTypeDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<PassengerType>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<PassengerTypeDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PassengerTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PassengerTypeDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<PassengerType>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<PassengerTypeDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(PassengerTypeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PassengerTypeDto>> Create([FromBody] CreatePassengerTypeRequest req, CancellationToken ct)
    {
        var item = new PassengerType(req.Nombre, req.EdadMin, req.EdadMax);
        await _uow.Repository<PassengerType>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<PassengerTypeDto>(item));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePassengerTypeRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<PassengerType>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Nombre, req.EdadMin, req.EdadMax);
        await _uow.Repository<PassengerType>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<PassengerType>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<PassengerType>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
