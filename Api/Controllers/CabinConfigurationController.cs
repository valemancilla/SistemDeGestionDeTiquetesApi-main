using Api.Dtos.Cabin;
using Application.Abstractions;
using Domain.Entities.Aircraft;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("CabinConfiguration")]
[Route("api/cabin-configurations")]
public sealed class CabinConfigurationController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CabinConfigurationController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CabinConfigurationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CabinConfigurationDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<CabinConfiguration>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<CabinConfigurationDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CabinConfigurationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CabinConfigurationDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<CabinConfiguration>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<CabinConfigurationDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CabinConfigurationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CabinConfigurationDto>> Create([FromBody] CreateCabinConfigurationRequest req, CancellationToken ct)
    {
        var item = new CabinConfiguration(req.AeronaveId, req.TipoCabinaId, req.FilaInicio,
            req.FilaFin, req.AsientosPorFila, req.LetrasAsientos);
        await _uow.Repository<CabinConfiguration>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<CabinConfigurationDto>((await _uow.Repository<CabinConfiguration>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCabinConfigurationRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<CabinConfiguration>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.FilaInicio, req.FilaFin, req.AsientosPorFila, req.LetrasAsientos);
        await _uow.Repository<CabinConfiguration>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<CabinConfiguration>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<CabinConfiguration>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
