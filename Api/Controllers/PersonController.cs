using Api.Dtos.People;
using Application.Abstractions;
using Domain.Entities.People;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Person")]
[Route("api/persons")]
public sealed class PersonController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PersonController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PersonDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PersonDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<Person>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<PersonDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Person>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<PersonDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonDto>> Create([FromBody] CreatePersonRequest req, CancellationToken ct)
    {
        var genero = req.Genero is { Length: > 0 } g ? (char?)g[0] : null;
        var item = new Person(req.TipoDocumentoId, req.NumeroDocumento, req.Nombres,
            req.Apellidos, req.FechaNacimiento, genero, req.DireccionId);
        await _uow.Repository<Person>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<PersonDto>((await _uow.Repository<Person>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<Person>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        var genero = req.Genero is { Length: > 0 } g ? (char?)g[0] : null;
        item.Update(req.Nombres, req.Apellidos, genero, req.DireccionId);
        await _uow.Repository<Person>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Person>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<Person>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
