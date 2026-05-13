using Api.Dtos.Invoices;
using Application.Abstractions;
using Domain.Entities.Invoices;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Invoice")]
[Route("api/invoices")]
public sealed class InvoiceController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public InvoiceController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<InvoiceDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<InvoiceDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Invoices.GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<InvoiceDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(InvoiceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvoiceDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Invoices.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<InvoiceDto>(item));
    }

    [HttpGet("by-numero/{numero}")]
    [ProducesResponseType(typeof(InvoiceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvoiceDto>> GetByNumero(string numero, CancellationToken ct)
    {
        var item = await _uow.Invoices.GetByNumeroAsync(numero, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<InvoiceDto>(item));
    }

    [HttpGet("by-reserva/{reservaId:int}")]
    [ProducesResponseType(typeof(InvoiceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvoiceDto>> GetByReserva(int reservaId, CancellationToken ct)
    {
        var item = await _uow.Invoices.GetByReservaAsync(reservaId, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<InvoiceDto>(item));
    }

    [HttpGet("{id:int}/items")]
    [ProducesResponseType(typeof(IReadOnlyList<InvoiceItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<InvoiceItemDto>>> GetItems(int id, CancellationToken ct)
    {
        var items = await _uow.Invoices.GetItemsByFacturaAsync(id, ct);
        return Ok(_mapper.Map<IReadOnlyList<InvoiceItemDto>>(items));
    }

    [HttpPost]
    [ProducesResponseType(typeof(InvoiceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<InvoiceDto>> Create([FromBody] CreateInvoiceRequest req, CancellationToken ct)
    {
        if (await _uow.Invoices.ExistsNumeroFacturaAsync(req.NumeroFactura, ct))
            return Conflict();

        var item = new Invoice(req.ReservaId, req.NumeroFactura, req.Subtotal, req.Impuestos);
        await _uow.Invoices.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        var created = await _uow.Invoices.GetByIdAsync(item.Id, ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<InvoiceDto>(created!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateInvoiceRequest req, CancellationToken ct)
    {
        var item = await _uow.Invoices.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.RecalcularTotal(req.Subtotal, req.Impuestos);
        await _uow.Invoices.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Invoices.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Invoices.RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
