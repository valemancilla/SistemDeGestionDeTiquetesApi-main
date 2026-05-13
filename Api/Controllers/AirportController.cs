using Api.Dtos.Airport;
using Application.Abstractions;
using Domain.Entities.Airports;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Airport")]
[Route("api/airports")]
public sealed class AirportController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public AirportController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AirportDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AirportDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<Airport>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<AirportDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AirportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AirportDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Airport>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<AirportDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(AirportDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AirportDto>> Create([FromBody] CreateAirportRequest req, CancellationToken ct)
    {
        var item = new Airport(req.Nombre, req.CodigoIata, req.CiudadId, req.CodigoIcao);
        await _uow.Repository<Airport>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<AirportDto>((await _uow.Repository<Airport>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAirportRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<Airport>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Nombre, req.CodigoIcao);
        await _uow.Repository<Airport>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Airport>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<Airport>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
