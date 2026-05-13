using Api.Dtos.Payments;
using Application.Abstractions;
using Domain.Entities.Payments;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("PaymentMethod")]
[Route("api/payment-methods")]
public sealed class PaymentMethodController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PaymentMethodController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PaymentMethodDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PaymentMethodDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<PaymentMethod>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<PaymentMethodDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PaymentMethodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentMethodDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<PaymentMethod>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<PaymentMethodDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaymentMethodDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaymentMethodDto>> Create([FromBody] CreatePaymentMethodRequest req, CancellationToken ct)
    {
        var item = new PaymentMethod(req.TipoMedioPagoId, req.NombreComercial, req.TipoTarjetaId, req.EmisorTarjetaId);
        await _uow.Repository<PaymentMethod>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<PaymentMethodDto>((await _uow.Repository<PaymentMethod>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePaymentMethodRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<PaymentMethod>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.NombreComercial, req.TipoTarjetaId, req.EmisorTarjetaId);
        await _uow.Repository<PaymentMethod>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<PaymentMethod>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<PaymentMethod>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
