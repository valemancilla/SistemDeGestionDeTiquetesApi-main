using Api.Dtos.Geography;
using Application.Abstractions;
using Domain.Entities.Geography;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("City")]
[Route("api/cities")]
public sealed class CityController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CityController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CityDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CityDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<City>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<CityDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CityDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<City>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<CityDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CityDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CityDto>> Create([FromBody] CreateCityRequest req, CancellationToken ct)
    {
        var item = new City(req.Nombre, req.RegionId);
        await _uow.Repository<City>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<CityDto>((await _uow.Repository<City>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] string nombre, CancellationToken ct)
    {
        var item = await _uow.Repository<City>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(nombre);
        await _uow.Repository<City>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<City>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<City>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
