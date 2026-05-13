using Api.Dtos.Addresses;
using Application.Abstractions;
using Domain.Entities.Addresses;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Tags("Address")]
[Route("api/addresses")]
public sealed class AddressController : BaseApiController
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public AddressController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AddressDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AddressDto>>> GetAll(CancellationToken ct)
    {
        var items = await _uow.Repository<Address>().GetAllAsync(ct);
        return Ok(_mapper.Map<IReadOnlyList<AddressDto>>(items));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AddressDto>> GetById(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Address>().GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(_mapper.Map<AddressDto>(item));
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddressDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AddressDto>> Create([FromBody] CreateAddressRequest req, CancellationToken ct)
    {
        var item = new Address(req.TipoViaId, req.NombreVia, req.CiudadId,
            req.Numero, req.Complemento, req.CodigoPostal);
        await _uow.Repository<Address>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, _mapper.Map<AddressDto>((await _uow.Repository<Address>().GetByIdAsync(item.Id, ct))!));
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAddressRequest req, CancellationToken ct)
    {
        var item = await _uow.Repository<Address>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        item.Update(req.TipoViaId, req.NombreVia, req.CiudadId,
            req.Numero, req.Complemento, req.CodigoPostal);
        await _uow.Repository<Address>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var item = await _uow.Repository<Address>().GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        await _uow.Repository<Address>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return NoContent();
    }
}
