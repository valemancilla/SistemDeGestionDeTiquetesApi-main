using Application.Abstractions;
using Domain.Entities.Flights;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Flights;

public sealed record GetSeatLocationTypes : IRequest<IReadOnlyList<SeatLocationType>>;
public sealed class GetSeatLocationTypesHandler : IRequestHandler<GetSeatLocationTypes, IReadOnlyList<SeatLocationType>>
{
    private readonly IUnitOfWork _uow;
    public GetSeatLocationTypesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<SeatLocationType>> Handle(GetSeatLocationTypes _, CancellationToken ct)
        => _uow.Repository<SeatLocationType>().GetAllAsync(ct);
}

public sealed record GetSeatLocationTypeById(int Id) : IRequest<SeatLocationType?>;
public sealed class GetSeatLocationTypeByIdHandler : IRequestHandler<GetSeatLocationTypeById, SeatLocationType?>
{
    private readonly IUnitOfWork _uow;
    public GetSeatLocationTypeByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<SeatLocationType?> Handle(GetSeatLocationTypeById req, CancellationToken ct)
        => _uow.Repository<SeatLocationType>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateSeatLocationType(string Nombre) : IRequest<int>;
public sealed class CreateSeatLocationTypeHandler : IRequestHandler<CreateSeatLocationType, int>
{
    private readonly IUnitOfWork _uow;
    public CreateSeatLocationTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateSeatLocationType req, CancellationToken ct)
    {
        var item = new SeatLocationType(req.Nombre);
        await _uow.Repository<SeatLocationType>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateSeatLocationTypeValidator : AbstractValidator<CreateSeatLocationType>
{
    public CreateSeatLocationTypeValidator() => RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
}

public sealed record UpdateSeatLocationType(int Id, string Nombre) : IRequest;
public sealed class UpdateSeatLocationTypeHandler : IRequestHandler<UpdateSeatLocationType>
{
    private readonly IUnitOfWork _uow;
    public UpdateSeatLocationTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateSeatLocationType req, CancellationToken ct)
    {
        var item = await _uow.Repository<SeatLocationType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"SeatLocationType {req.Id} not found.");
        item.Update(req.Nombre);
        await _uow.Repository<SeatLocationType>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteSeatLocationType(int Id) : IRequest;
public sealed class DeleteSeatLocationTypeHandler : IRequestHandler<DeleteSeatLocationType>
{
    private readonly IUnitOfWork _uow;
    public DeleteSeatLocationTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteSeatLocationType req, CancellationToken ct)
    {
        var item = await _uow.Repository<SeatLocationType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"SeatLocationType {req.Id} not found.");
        await _uow.Repository<SeatLocationType>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}