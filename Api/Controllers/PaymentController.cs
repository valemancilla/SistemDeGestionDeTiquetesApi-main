using Api.Dtos.Payments;
using Application.Abstractions;
using Domain.Entities.Payments;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Payment")]
[Route("api/payments")]
public sealed class PaymentController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PaymentController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PaymentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PaymentDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Payments.GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<PaymentDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Payments.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<PaymentDto>(item));
    }

    [HttpGet("by-reserva/{reservaId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<PaymentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PaymentDto>>> GetByReserva(int reservaId, CancellationToken ct)
    {
        var items = await _uow.Payments.GetByReservaAsync(reservaId, ct);
        return Ok(_mapper.Map<IReadOnlyList<PaymentDto>>(items));
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaymentDto>> Create([FromBody] CreatePaymentRequest req, CancellationToken ct)
    {
        var item = new Payment(req.ReservaId, req.Monto, req.FechaPago, req.EstadoPagoId, req.MetodoPagoId);
        await _uow.Payments.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        var created = await _uow.Payments.GetByIdAsync(item.Id, ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<PaymentDto>(created!));
    }

    [HttpPut("{id:int}/estado")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEstado(int id, [FromBody] UpdatePaymentStatusRequest req, CancellationToken ct)
    {
        var item = await _uow.Payments.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.CambiarEstado(req.EstadoPagoId);
        await _uow.Payments.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Payments.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Payments.RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
