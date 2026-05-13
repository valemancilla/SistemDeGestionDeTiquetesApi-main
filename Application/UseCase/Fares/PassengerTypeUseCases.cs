using Application.Abstractions;
using Domain.Entities.Fares;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Fares;

public sealed record GetPassengerTypes : IRequest<IReadOnlyList<PassengerType>>;
public sealed class GetPassengerTypesHandler : IRequestHandler<GetPassengerTypes, IReadOnlyList<PassengerType>>
{
    private readonly IUnitOfWork _uow;
    public GetPassengerTypesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<PassengerType>> Handle(GetPassengerTypes _, CancellationToken ct)
        => _uow.Repository<PassengerType>().GetAllAsync(ct);
}

public sealed record GetPassengerTypeById(int Id) : IRequest<PassengerType?>;
public sealed class GetPassengerTypeByIdHandler : IRequestHandler<GetPassengerTypeById, PassengerType?>
{
    private readonly IUnitOfWork _uow;
    public GetPassengerTypeByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<PassengerType?> Handle(GetPassengerTypeById req, CancellationToken ct)
        => _uow.Repository<PassengerType>().GetByIdAsync(req.Id, ct);
}

public sealed record CreatePassengerType(string Nombre, int? EdadMin, int? EdadMax) : IRequest<int>;
public sealed class CreatePassengerTypeHandler : IRequestHandler<CreatePassengerType, int>
{
    private readonly IUnitOfWork _uow;
    public CreatePassengerTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreatePassengerType req, CancellationToken ct)
    {
        var item = new PassengerType(req.Nombre, req.EdadMin, req.EdadMax);
        await _uow.Repository<PassengerType>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreatePassengerTypeValidator : AbstractValidator<CreatePassengerType>
{
    public CreatePassengerTypeValidator() => RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
}

public sealed record UpdatePassengerType(int Id, string Nombre, int? EdadMin, int? EdadMax) : IRequest;
public sealed class UpdatePassengerTypeHandler : IRequestHandler<UpdatePassengerType>
{
    private readonly IUnitOfWork _uow;
    public UpdatePassengerTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdatePassengerType req, CancellationToken ct)
    {
        var item = await _uow.Repository<PassengerType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"PassengerType {req.Id} not found.");
        item.Update(req.Nombre, req.EdadMin, req.EdadMax);
        await _uow.Repository<PassengerType>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeletePassengerType(int Id) : IRequest;
public sealed class DeletePassengerTypeHandler : IRequestHandler<DeletePassengerType>
{
    private readonly IUnitOfWork _uow;
    public DeletePassengerTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeletePassengerType req, CancellationToken ct)
    {
        var item = await _uow.Repository<PassengerType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"PassengerType {req.Id} not found.");
        await _uow.Repository<PassengerType>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}