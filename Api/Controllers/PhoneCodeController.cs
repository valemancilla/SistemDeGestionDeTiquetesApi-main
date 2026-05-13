using Api.Dtos.People;
using Application.Abstractions;
using Domain.Entities.People;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("PhoneCode")]
[Route("api/phone-codes")]
public sealed class PhoneCodeController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PhoneCodeController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PhoneCodeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PhoneCodeDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<PhoneCode>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<PhoneCodeDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PhoneCodeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PhoneCodeDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<PhoneCode>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<PhoneCodeDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(PhoneCodeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PhoneCodeDto>> Create([FromBody] CreatePhoneCodeRequest req, CancellationToken ct)
    {
        var item = new PhoneCode(req.CodigoPais, req.NombrePais);
        await _uow.Repository<PhoneCode>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<PhoneCodeDto>(item));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePhoneCodeRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<PhoneCode>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.CodigoPais, req.NombrePais);
        await _uow.Repository<PhoneCode>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<PhoneCode>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<PhoneCode>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
