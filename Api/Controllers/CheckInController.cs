using Api.Dtos.CheckIn;
using Application.Abstractions;
using Domain.Entities.CheckIn;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("CheckIn")]
[Route("api/checkins")]
public sealed class CheckInController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CheckInController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CheckInDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CheckInDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.CheckIns.GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<CheckInDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CheckInDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CheckInDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.CheckIns.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<CheckInDto>(item));
    }

    [HttpGet("by-tiquete/{tiqueteId:int}")]
    [ProducesResponseType(typeof(CheckInDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CheckInDto>> GetByTiquete(int tiqueteId, CancellationToken ct)
    {
        var item = await _uow.CheckIns.GetByTiqueteAsync(tiqueteId, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<CheckInDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CheckInDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CheckInDto>> Create([FromBody] CreateCheckInRequest req, CancellationToken ct)
    {
        var item = new CheckIn(req.TiqueteId, req.PersonalId, req.AsientoVueloId, req.FechaCheckin,
            req.EstadoCheckinId, req.NumeroTarjetaEmbarque, req.EquipajeBodega, req.PesoEquipajeKg);
        await _uow.CheckIns.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        var created = await _uow.CheckIns.GetByIdAsync(item.Id, ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<CheckInDto>(created!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCheckInRequest req, CancellationToken ct)
    {
        var item = await _uow.CheckIns.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.EstadoCheckinId, req.EquipajeBodega, req.PesoEquipajeKg);
        await _uow.CheckIns.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
