using Application.Abstractions;
using Domain.Entities.Addresses;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Addresses;

public sealed record GetRoadTypes : IRequest<IReadOnlyList<RoadType>>;
public sealed class GetRoadTypesHandler : IRequestHandler<GetRoadTypes, IReadOnlyList<RoadType>>
{
    private readonly IUnitOfWork _uow;
    public GetRoadTypesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<RoadType>> Handle(GetRoadTypes _, CancellationToken ct)
        => _uow.Repository<RoadType>().GetAllAsync(ct);
}

public sealed record GetRoadTypeById(int Id) : IRequest<RoadType?>;
public sealed class GetRoadTypeByIdHandler : IRequestHandler<GetRoadTypeById, RoadType?>
{
    private readonly IUnitOfWork _uow;
    public GetRoadTypeByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<RoadType?> Handle(GetRoadTypeById req, CancellationToken ct)
        => _uow.Repository<RoadType>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateRoadType(string Nombre) : IRequest<int>;
public sealed class CreateRoadTypeHandler : IRequestHandler<CreateRoadType, int>
{
    private readonly IUnitOfWork _uow;
    public CreateRoadTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateRoadType req, CancellationToken ct)
    {
        var item = new RoadType(req.Nombre);
        await _uow.Repository<RoadType>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateRoadTypeValidator : AbstractValidator<CreateRoadType>
{
    public CreateRoadTypeValidator() => RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
}

public sealed record UpdateRoadType(int Id, string Nombre) : IRequest;
public sealed class UpdateRoadTypeHandler : IRequestHandler<UpdateRoadType>
{
    private readonly IUnitOfWork _uow;
    public UpdateRoadTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateRoadType req, CancellationToken ct)
    {
        var item = await _uow.Repository<RoadType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"RoadType {req.Id} not found.");
        item.Update(req.Nombre);
        await _uow.Repository<RoadType>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
public sealed class UpdateRoadTypeValidator : AbstractValidator<UpdateRoadType>
{
    public UpdateRoadTypeValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
    }
}

public sealed record DeleteRoadType(int Id) : IRequest;
public sealed class DeleteRoadTypeHandler : IRequestHandler<DeleteRoadType>
{
    private readonly IUnitOfWork _uow;
    public DeleteRoadTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteRoadType req, CancellationToken ct)
    {
        var item = await _uow.Repository<RoadType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"RoadType {req.Id} not found.");
        await _uow.Repository<RoadType>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}