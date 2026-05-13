using Application.Abstractions;
using Domain.Entities.Aircraft;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Aircraft;

public sealed record GetAircraftManufacturers : IRequest<IReadOnlyList<AircraftManufacturer>>;
public sealed class GetAircraftManufacturersHandler : IRequestHandler<GetAircraftManufacturers, IReadOnlyList<AircraftManufacturer>>
{
    private readonly IUnitOfWork _uow;
    public GetAircraftManufacturersHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<AircraftManufacturer>> Handle(GetAircraftManufacturers _, CancellationToken ct)
        => _uow.Repository<AircraftManufacturer>().GetAllAsync(ct);
}

public sealed record GetAircraftManufacturerById(int Id) : IRequest<AircraftManufacturer?>;
public sealed class GetAircraftManufacturerByIdHandler : IRequestHandler<GetAircraftManufacturerById, AircraftManufacturer?>
{
    private readonly IUnitOfWork _uow;
    public GetAircraftManufacturerByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<AircraftManufacturer?> Handle(GetAircraftManufacturerById req, CancellationToken ct)
        => _uow.Repository<AircraftManufacturer>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateAircraftManufacturer(string Nombre, int PaisId) : IRequest<int>;
public sealed class CreateAircraftManufacturerHandler : IRequestHandler<CreateAircraftManufacturer, int>
{
    private readonly IUnitOfWork _uow;
    public CreateAircraftManufacturerHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateAircraftManufacturer req, CancellationToken ct)
    {
        var item = new AircraftManufacturer(req.Nombre, req.PaisId);
        await _uow.Repository<AircraftManufacturer>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateAircraftManufacturerValidator : AbstractValidator<CreateAircraftManufacturer>
{
    public CreateAircraftManufacturerValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PaisId).GreaterThan(0);
    }
}

public sealed record UpdateAircraftManufacturer(int Id, string Nombre, int PaisId) : IRequest;
public sealed class UpdateAircraftManufacturerHandler : IRequestHandler<UpdateAircraftManufacturer>
{
    private readonly IUnitOfWork _uow;
    public UpdateAircraftManufacturerHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateAircraftManufacturer req, CancellationToken ct)
    {
        var item = await _uow.Repository<AircraftManufacturer>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"AircraftManufacturer {req.Id} not found.");
        item.Update(req.Nombre, req.PaisId);
        await _uow.Repository<AircraftManufacturer>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
public sealed class UpdateAircraftManufacturerValidator : AbstractValidator<UpdateAircraftManufacturer>
{
    public UpdateAircraftManufacturerValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PaisId).GreaterThan(0);
    }
}

public sealed record DeleteAircraftManufacturer(int Id) : IRequest;
public sealed class DeleteAircraftManufacturerHandler : IRequestHandler<DeleteAircraftManufacturer>
{
    private readonly IUnitOfWork _uow;
    public DeleteAircraftManufacturerHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteAircraftManufacturer req, CancellationToken ct)
    {
        var item = await _uow.Repository<AircraftManufacturer>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"AircraftManufacturer {req.Id} not found.");
        await _uow.Repository<AircraftManufacturer>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}