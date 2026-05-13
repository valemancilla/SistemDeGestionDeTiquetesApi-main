using Api.Dtos.People;
using Application.Abstractions;
using Domain.Entities.People;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("PersonEmail")]
[Route("api/person-emails")]
public sealed class PersonEmailController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PersonEmailController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PersonEmailDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PersonEmailDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<PersonEmail>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<PersonEmailDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PersonEmailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonEmailDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<PersonEmail>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<PersonEmailDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(PersonEmailDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonEmailDto>> Create([FromBody] CreatePersonEmailRequest req, CancellationToken ct)
    {
        var item = new PersonEmail(req.PersonaId, req.UsuarioEmail, req.DominioEmailId, req.EsPrincipal);
        await _uow.Repository<PersonEmail>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<PersonEmailDto>((await _uow.Repository<PersonEmail>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonEmailRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<PersonEmail>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.UsuarioEmail, req.DominioEmailId, req.EsPrincipal);
        await _uow.Repository<PersonEmail>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<PersonEmail>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<PersonEmail>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
