using Api.Dtos.Common;
using Application.Abstractions;
using Domain.Entities.Addresses;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("RoadType")]
[Route("api/road-types")]
public sealed class RoadTypeController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public RoadTypeController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<LookupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<LookupDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<RoadType>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<LookupDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LookupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LookupDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<RoadType>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<LookupDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(LookupDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LookupDto>> Create([FromBody] string nombre, CancellationToken ct)
    {
        var item = new RoadType(nombre);
        await _uow.Repository<RoadType>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<LookupDto>(item));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] string nombre, CancellationToken ct)
    {
        var item = await _uow.Repository<RoadType>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(nombre);
        await _uow.Repository<RoadType>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<RoadType>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<RoadType>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
