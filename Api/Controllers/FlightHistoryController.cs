using Api.Dtos.Flights;
using Application.Abstractions;
using Domain.Entities.Flights;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("FlightHistory")]
[Route("api/flight-history")]
public sealed class FlightHistoryController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public FlightHistoryController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<FlightHistoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<FlightHistoryDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<FlightHistory>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<FlightHistoryDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FlightHistoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FlightHistoryDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightHistory>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<FlightHistoryDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(FlightHistoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FlightHistoryDto>> Create([FromBody] CreateFlightHistoryRequest req, CancellationToken ct)
    {
        var item = new FlightHistory(req.VueloId, req.EstadoAnteriorId, req.EstadoNuevoId, req.CambiadoPor, req.Observacion);
        await _uow.Repository<FlightHistory>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<FlightHistoryDto>((await _uow.Repository<FlightHistory>().GetByIdAsync(item.Id, ct))!));
    }
}
