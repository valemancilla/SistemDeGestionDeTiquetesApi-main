using Api.Dtos.Aircraft;
using Application.Abstractions;
using Domain.Entities.Aircraft;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("AircraftManufacturer")]
[Route("api/aircraft-manufacturers")]
public sealed class AircraftManufacturerController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public AircraftManufacturerController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AircraftManufacturerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AircraftManufacturerDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<AircraftManufacturer>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<AircraftManufacturerDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AircraftManufacturerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AircraftManufacturerDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<AircraftManufacturer>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<AircraftManufacturerDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(AircraftManufacturerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AircraftManufacturerDto>> Create([FromBody] CreateAircraftManufacturerRequest req, CancellationToken ct)
    {
        var item = new AircraftManufacturer(req.Nombre, req.PaisId);
        await _uow.Repository<AircraftManufacturer>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<AircraftManufacturerDto>(item));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAircraftManufacturerRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<AircraftManufacturer>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Nombre, req.PaisId);
        await _uow.Repository<AircraftManufacturer>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<AircraftManufacturer>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<AircraftManufacturer>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
