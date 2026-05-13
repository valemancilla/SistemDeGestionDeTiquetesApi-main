using Api.Dtos.Fares;
using Application.Abstractions;
using Domain.Entities.Fares;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Fare")]
[Route("api/fares")]
public sealed class FareController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public FareController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<FareDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<FareDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<Fare>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<FareDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FareDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FareDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Fare>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<FareDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(FareDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FareDto>> Create([FromBody] CreateFareRequest req, CancellationToken ct)
    {
        var item = new Fare(req.RutaId, req.TipoCabinaId, req.TipoPasajeroId, req.TemporadaId,
            req.PrecioBase, req.VigenciaDesde, req.VigenciaHasta);
        await _uow.Repository<Fare>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<FareDto>((await _uow.Repository<Fare>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateFareRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<Fare>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.PrecioBase, req.VigenciaDesde, req.VigenciaHasta);
        await _uow.Repository<Fare>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Fare>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<Fare>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
