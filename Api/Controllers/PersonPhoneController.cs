using Api.Dtos.People;
using Application.Abstractions;
using Domain.Entities.People;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("PersonPhone")]
[Route("api/person-phones")]
public sealed class PersonPhoneController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PersonPhoneController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PersonPhoneDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PersonPhoneDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<PersonPhone>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<PersonPhoneDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PersonPhoneDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonPhoneDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<PersonPhone>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<PersonPhoneDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(PersonPhoneDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonPhoneDto>> Create([FromBody] CreatePersonPhoneRequest req, CancellationToken ct)
    {
        var item = new PersonPhone(req.PersonaId, req.CodigoTelefonoId, req.NumeroTelefono, req.EsPrincipal);
        await _uow.Repository<PersonPhone>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<PersonPhoneDto>((await _uow.Repository<PersonPhone>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonPhoneRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<PersonPhone>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.CodigoTelefonoId, req.NumeroTelefono, req.EsPrincipal);
        await _uow.Repository<PersonPhone>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<PersonPhone>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<PersonPhone>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
