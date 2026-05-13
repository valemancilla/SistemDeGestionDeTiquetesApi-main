using Api.Dtos.People;
using Application.Abstractions;
using Domain.Entities.People;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("DocumentType")]
[Route("api/document-types")]
public sealed class DocumentTypeController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public DocumentTypeController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<DocumentTypeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<DocumentTypeDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<DocumentType>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<DocumentTypeDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(DocumentTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DocumentTypeDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<DocumentType>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<DocumentTypeDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(DocumentTypeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DocumentTypeDto>> Create([FromBody] CreateDocumentTypeRequest req, CancellationToken ct)
    {
        var item = new DocumentType(req.Nombre, req.Codigo);
        await _uow.Repository<DocumentType>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<DocumentTypeDto>(item));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDocumentTypeRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<DocumentType>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Nombre, req.Codigo);
        await _uow.Repository<DocumentType>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<DocumentType>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<DocumentType>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
