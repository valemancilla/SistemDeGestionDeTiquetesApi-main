using Api.Dtos.Airlines;
using Application.Abstractions;
using Domain.Entities.Airlines;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Airlines")]
[Route("api/airlines")]
public sealed class AirlinesController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public AirlinesController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AirlineDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AirlineDto>>> GetAll(
        [FromQuery] bool soloActivas = false, CancellationToken ct = default)
    {
        var items = soloActivas
            ? await _uow.Airlines.GetActivasAsync(ct)
            : await _uow.Airlines.GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<AirlineDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AirlineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AirlineDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Airlines.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<AirlineDto>(item));
    }

    [HttpGet("by-iata/{codigoIata}")]
    [ProducesResponseType(typeof(AirlineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AirlineDto>> GetByIata(string codigoIata, CancellationToken ct)
    {
        var item = await _uow.Airlines.GetByCodigoIataAsync(codigoIata, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<AirlineDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(AirlineDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AirlineDto>> Create([FromBody] CreateAirlineRequest req, CancellationToken ct)
    {
        if (await _uow.Airlines.ExistsCodigoIataAsync(req.CodigoIata, ct))
            return Conflict();

        var item = new Airline(req.Nombre, req.CodigoIata, req.PaisOrigenId);
        await _uow.Airlines.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        var created = await _uow.Airlines.GetByIdAsync(item.Id, ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<AirlineDto>(created!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAirlineRequest req, CancellationToken ct)
    {
        var item = await _uow.Airlines.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Nombre, req.Activa);
        await _uow.Airlines.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Airlines.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(item.Nombre, activa: false);
        await _uow.Airlines.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
