using Api.Dtos.Flights;
using Application.Abstractions;
using Domain.Entities.Flights;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Flights")]
[Route("api/flights")]
public sealed class FlightsController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public FlightsController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<FlightDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<FlightDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Flights.GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<FlightDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FlightDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FlightDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Flights.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<FlightDto>(item));
    }

    [HttpGet("by-ruta/{rutaId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<FlightDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<FlightDto>>> GetByRuta(int rutaId, CancellationToken ct)
    {
        var items = await _uow.Flights.GetByRutaAsync(rutaId, ct);
        return Ok(_mapper.Map<IReadOnlyList<FlightDto>>(items));
    }

    [HttpGet("by-codigo/{codigo}")]
    [ProducesResponseType(typeof(FlightDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FlightDto>> GetByCodigo(string codigo, CancellationToken ct)
    {
        var item = await _uow.Flights.GetByCodigoAsync(codigo, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<FlightDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(FlightDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<FlightDto>> Create([FromBody] CreateFlightRequest req, CancellationToken ct)
    {
        if (await _uow.Flights.ExistsCodigoAsync(req.CodigoVuelo, ct))
            return Conflict();

        var item = new Flight(req.CodigoVuelo, req.AerolineaId, req.RutaId, req.AeronaveId,
            req.FechaSalida, req.FechaLlegadaEstimada, req.CapacidadTotal, req.EstadoVueloId);
        await _uow.Flights.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        var created = await _uow.Flights.GetByIdAsync(item.Id, ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<FlightDto>(created!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateFlightRequest req, CancellationToken ct)
    {
        var item = await _uow.Flights.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.AeronaveId, req.FechaSalida, req.FechaLlegadaEstimada);
        await _uow.Flights.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpPut("{id:int}/estado")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEstado(int id, [FromBody] UpdateFlightStateRequest req, CancellationToken ct)
    {
        var item = await _uow.Flights.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        var history = new FlightHistory(id, item.EstadoVueloId, req.NuevoEstadoId, req.CambiadoPor, req.Observacion);
        item.CambiarEstado(req.NuevoEstadoId, req.ReprogramadoEn);
        await _uow.Flights.UpdateAsync(item, ct);
        await _uow.Repository<FlightHistory>().AddAsync(history, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Flights.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<Flight>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
