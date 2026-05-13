using Application.Abstractions;
using Domain.Entities.Addresses;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Addresses;

public sealed record GetAddresses : IRequest<IReadOnlyList<Address>>;
public sealed class GetAddressesHandler : IRequestHandler<GetAddresses, IReadOnlyList<Address>>
{
    private readonly IUnitOfWork _uow;
    public GetAddressesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<Address>> Handle(GetAddresses _, CancellationToken ct)
        => _uow.Repository<Address>().GetAllAsync(ct);
}

public sealed record GetAddressById(int Id) : IRequest<Address?>;
public sealed class GetAddressByIdHandler : IRequestHandler<GetAddressById, Address?>
{
    private readonly IUnitOfWork _uow;
    public GetAddressByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<Address?> Handle(GetAddressById req, CancellationToken ct)
        => _uow.Repository<Address>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateAddress(
    int TipoViaId, string NombreVia, int CiudadId,
    string? Numero, string? Complemento, string? CodigoPostal) : IRequest<int>;
public sealed class CreateAddressHandler : IRequestHandler<CreateAddress, int>
{
    private readonly IUnitOfWork _uow;
    public CreateAddressHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateAddress req, CancellationToken ct)
    {
        var item = new Address(req.TipoViaId, req.NombreVia, req.CiudadId, req.Numero, req.Complemento, req.CodigoPostal);
        await _uow.Repository<Address>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateAddressValidator : AbstractValidator<CreateAddress>
{
    public CreateAddressValidator()
    {
        RuleFor(x => x.TipoViaId).GreaterThan(0);
        RuleFor(x => x.NombreVia).NotEmpty().MaximumLength(200);
        RuleFor(x => x.CiudadId).GreaterThan(0);
        RuleFor(x => x.CodigoPostal)
            .Length(1, 20)
            .Matches("^[A-Za-z0-9 \\-]+$")
            .When(x => !string.IsNullOrWhiteSpace(x.CodigoPostal));
    }
}

public sealed record UpdateAddress(
    int Id, int TipoViaId, string NombreVia, int CiudadId,
    string? Numero, string? Complemento, string? CodigoPostal) : IRequest;
public sealed class UpdateAddressHandler : IRequestHandler<UpdateAddress>
{
    private readonly IUnitOfWork _uow;
    public UpdateAddressHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateAddress req, CancellationToken ct)
    {
        var item = await _uow.Repository<Address>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Address {req.Id} not found.");
        item.Update(req.TipoViaId, req.NombreVia, req.CiudadId, req.Numero, req.Complemento, req.CodigoPostal);
        await _uow.Repository<Address>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
public sealed class UpdateAddressValidator : AbstractValidator<UpdateAddress>
{
    public UpdateAddressValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.TipoViaId).GreaterThan(0);
        RuleFor(x => x.NombreVia).NotEmpty().MaximumLength(200);
        RuleFor(x => x.CiudadId).GreaterThan(0);
        RuleFor(x => x.CodigoPostal)
            .Length(1, 20)
            .Matches("^[A-Za-z0-9 \\-]+$")
            .When(x => !string.IsNullOrWhiteSpace(x.CodigoPostal));
    }
}

public sealed record DeleteAddress(int Id) : IRequest;
public sealed class DeleteAddressHandler : IRequestHandler<DeleteAddress>
{
    private readonly IUnitOfWork _uow;
    public DeleteAddressHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteAddress req, CancellationToken ct)
    {
        var item = await _uow.Repository<Address>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Address {req.Id} not found.");
        await _uow.Repository<Address>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}