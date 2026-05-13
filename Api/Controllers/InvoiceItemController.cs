using Api.Dtos.Invoices;
using Application.Abstractions;
using Domain.Entities.Invoices;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("InvoiceItem")]
[Route("api/invoice-items")]
public sealed class InvoiceItemController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public InvoiceItemController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<InvoiceItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<InvoiceItemDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<InvoiceItem>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<InvoiceItemDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(InvoiceItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvoiceItemDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<InvoiceItem>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<InvoiceItemDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(InvoiceItemDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InvoiceItemDto>> Create([FromBody] CreateInvoiceItemRequest req, CancellationToken ct)
    {
        var item = new InvoiceItem(req.FacturaId, req.TipoItemId, req.Descripcion,
            req.Cantidad, req.PrecioUnitario, req.ReservaPasajeroId);
        await _uow.Repository<InvoiceItem>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<InvoiceItemDto>((await _uow.Repository<InvoiceItem>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateInvoiceItemRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<InvoiceItem>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Cantidad, req.PrecioUnitario, req.Descripcion);
        await _uow.Repository<InvoiceItem>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<InvoiceItem>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<InvoiceItem>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
