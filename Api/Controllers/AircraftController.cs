using Api.Dtos.Aircraft;
using Application.Abstractions;
using Domain.Entities.Aircraft;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Aircraft")]
[Route("api/aircraft")]
public sealed class AircraftController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public AircraftController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AircraftDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AircraftDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Aircraft.GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<AircraftDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AircraftDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AircraftDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Aircraft.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<AircraftDto>(item));
    }

    [HttpGet("by-aerolinea/{aerolineaId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<AircraftDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AircraftDto>>> GetByAerolinea(int aerolineaId, CancellationToken ct)
    {
        var items = await _uow.Aircraft.GetByAerolineaAsync(aerolineaId, ct);
        return Ok(_mapper.Map<IReadOnlyList<AircraftDto>>(items));
    }

    [HttpGet("by-matricula/{matricula}")]
    [ProducesResponseType(typeof(AircraftDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AircraftDto>> GetByMatricula(string matricula, CancellationToken ct)
    {
        var item = await _uow.Aircraft.GetByMatriculaAsync(matricula, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<AircraftDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(AircraftDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AircraftDto>> Create([FromBody] CreateAircraftRequest req, CancellationToken ct)
    {
        if (await _uow.Aircraft.ExistsMatriculaAsync(req.Matricula, ct))
            return Conflict();

        var item = new Aircraft(req.ModeloId, req.AerolineaId, req.Matricula, req.FechaFabricacion);
        await _uow.Aircraft.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        var created = await _uow.Aircraft.GetByIdAsync(item.Id, ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<AircraftDto>(created!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAircraftRequest req, CancellationToken ct)
    {
        var item = await _uow.Aircraft.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.ModeloId, req.AerolineaId, req.FechaFabricacion, req.Activa);
        await _uow.Aircraft.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Aircraft.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<Aircraft>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
