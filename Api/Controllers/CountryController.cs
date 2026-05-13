using Api.Dtos.Geography;
using Application.Abstractions;
using Domain.Entities.Geography;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Country")]
[Route("api/countries")]
public sealed class CountryController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CountryController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CountryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CountryDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<Country>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<CountryDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CountryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CountryDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Country>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<CountryDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CountryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CountryDto>> Create([FromBody] CreateCountryRequest req, CancellationToken ct)
    {
        var item = new Country(req.Nombre, req.CodigoIso, req.ContinenteId);
        await _uow.Repository<Country>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<CountryDto>((await _uow.Repository<Country>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCountryRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<Country>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Nombre, req.CodigoIso);
        await _uow.Repository<Country>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Country>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<Country>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
