using Api.Dtos.People;
using Application.Abstractions;
using Domain.Entities.People;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("EmailDomain")]
[Route("api/email-domains")]
public sealed class EmailDomainController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public EmailDomainController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<EmailDomainDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<EmailDomainDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<EmailDomain>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<EmailDomainDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(EmailDomainDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmailDomainDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<EmailDomain>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<EmailDomainDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmailDomainDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmailDomainDto>> Create([FromBody] CreateEmailDomainRequest req, CancellationToken ct)
    {
        var item = new EmailDomain(req.Dominio);
        await _uow.Repository<EmailDomain>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<EmailDomainDto>(item));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] CreateEmailDomainRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<EmailDomain>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.Dominio);
        await _uow.Repository<EmailDomain>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<EmailDomain>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<EmailDomain>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
