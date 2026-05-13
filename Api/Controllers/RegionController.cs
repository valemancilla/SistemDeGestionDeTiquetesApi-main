using Api.Dtos.Geography;
using Application.Abstractions;
using Domain.Entities.Geography;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Region")]
[Route("api/regions")]
public sealed class RegionController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public RegionController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<RegionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RegionDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<Region>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<RegionDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RegionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RegionDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Region>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<RegionDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RegionDto>> Create([FromBody] CreateRegionRequest req, CancellationToken ct)
    {
        var item = new Region(req.Nombre, req.Tipo, req.PaisId);
        await _uow.Repository<Region>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<RegionDto>((await _uow.Repository<Region>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRegionRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<Region>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Nombre, req.Tipo);
        await _uow.Repository<Region>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Region>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<Region>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
