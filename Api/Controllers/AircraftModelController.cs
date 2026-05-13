using Api.Dtos.Aircraft;
using Application.Abstractions;
using Domain.Entities.Aircraft;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("AircraftModel")]
[Route("api/aircraft-models")]
public sealed class AircraftModelController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public AircraftModelController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AircraftModelDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AircraftModelDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<AircraftModel>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<AircraftModelDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AircraftModelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AircraftModelDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<AircraftModel>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<AircraftModelDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(AircraftModelDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AircraftModelDto>> Create([FromBody] CreateAircraftModelRequest req, CancellationToken ct)
    {
        var item = new AircraftModel(req.FabricanteId, req.NombreModelo, req.CapacidadMaxima,
            req.PesoMaxDespegueKg, req.ConsumoCombustibleKgH, req.VelocidadCruceroKmh, req.AltitudCruceroFt);
        await _uow.Repository<AircraftModel>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<AircraftModelDto>((await _uow.Repository<AircraftModel>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAircraftModelRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<AircraftModel>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.NombreModelo, req.CapacidadMaxima, req.PesoMaxDespegueKg,
            req.ConsumoCombustibleKgH, req.VelocidadCruceroKmh, req.AltitudCruceroFt);
        await _uow.Repository<AircraftModel>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<AircraftModel>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<AircraftModel>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
