using Application.Abstractions;
using Domain.Entities.Aircraft;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Aircraft;

public sealed record GetCabinTypes : IRequest<IReadOnlyList<CabinType>>;
public sealed class GetCabinTypesHandler : IRequestHandler<GetCabinTypes, IReadOnlyList<CabinType>>
{
    private readonly IUnitOfWork _uow;
    public GetCabinTypesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<CabinType>> Handle(GetCabinTypes _, CancellationToken ct)
        => _uow.Repository<CabinType>().GetAllAsync(ct);
}

public sealed record GetCabinTypeById(int Id) : IRequest<CabinType?>;
public sealed class GetCabinTypeByIdHandler : IRequestHandler<GetCabinTypeById, CabinType?>
{
    private readonly IUnitOfWork _uow;
    public GetCabinTypeByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<CabinType?> Handle(GetCabinTypeById req, CancellationToken ct)
        => _uow.Repository<CabinType>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateCabinType(string Nombre) : IRequest<int>;
public sealed class CreateCabinTypeHandler : IRequestHandler<CreateCabinType, int>
{
    private readonly IUnitOfWork _uow;
    public CreateCabinTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateCabinType req, CancellationToken ct)
    {
        var item = new CabinType(req.Nombre);
        await _uow.Repository<CabinType>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateCabinTypeValidator : AbstractValidator<CreateCabinType>
{
    public CreateCabinTypeValidator() => RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
}

public sealed record UpdateCabinType(int Id, string Nombre) : IRequest;
public sealed class UpdateCabinTypeHandler : IRequestHandler<UpdateCabinType>
{
    private readonly IUnitOfWork _uow;
    public UpdateCabinTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateCabinType req, CancellationToken ct)
    {
        var item = await _uow.Repository<CabinType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"CabinType {req.Id} not found.");
        item.Update(req.Nombre);
        await _uow.Repository<CabinType>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteCabinType(int Id) : IRequest;
public sealed class DeleteCabinTypeHandler : IRequestHandler<DeleteCabinType>
{
    private readonly IUnitOfWork _uow;
    public DeleteCabinTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteCabinType req, CancellationToken ct)
    {
        var item = await _uow.Repository<CabinType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"CabinType {req.Id} not found.");
        await _uow.Repository<CabinType>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}