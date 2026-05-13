using Api.Dtos.Fares;
using Application.Abstractions;
using Domain.Entities.Fares;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Season")]
[Route("api/seasons")]
public sealed class SeasonController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public SeasonController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<SeasonDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SeasonDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<Season>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<SeasonDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SeasonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SeasonDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Season>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<SeasonDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(SeasonDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SeasonDto>> Create([FromBody] CreateSeasonRequest req, CancellationToken ct)
    {
        var item = new Season(req.Nombre, req.PrecioFactor, req.Descripcion);
        await _uow.Repository<Season>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<SeasonDto>(item));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSeasonRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<Season>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Nombre, req.PrecioFactor, req.Descripcion);
        await _uow.Repository<Season>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Season>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<Season>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
