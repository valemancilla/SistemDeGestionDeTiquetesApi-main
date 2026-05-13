using Application.Abstractions;
using Domain.Entities.Reservations;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Reservations;

public sealed record GetReservationStatuses : IRequest<IReadOnlyList<ReservationStatus>>;
public sealed class GetReservationStatusesHandler : IRequestHandler<GetReservationStatuses, IReadOnlyList<ReservationStatus>>
{
    private readonly IUnitOfWork _uow;
    public GetReservationStatusesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<ReservationStatus>> Handle(GetReservationStatuses _, CancellationToken ct)
        => _uow.Repository<ReservationStatus>().GetAllAsync(ct);
}

public sealed record GetReservationStatusById(int Id) : IRequest<ReservationStatus?>;
public sealed class GetReservationStatusByIdHandler : IRequestHandler<GetReservationStatusById, ReservationStatus?>
{
    private readonly IUnitOfWork _uow;
    public GetReservationStatusByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<ReservationStatus?> Handle(GetReservationStatusById req, CancellationToken ct)
        => _uow.Repository<ReservationStatus>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateReservationStatus(string Nombre) : IRequest<int>;
public sealed class CreateReservationStatusHandler : IRequestHandler<CreateReservationStatus, int>
{
    private readonly IUnitOfWork _uow;
    public CreateReservationStatusHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateReservationStatus req, CancellationToken ct)
    {
        var item = new ReservationStatus(req.Nombre);
        await _uow.Repository<ReservationStatus>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateReservationStatusValidator : AbstractValidator<CreateReservationStatus>
{
    public CreateReservationStatusValidator() => RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
}

public sealed record UpdateReservationStatus(int Id, string Nombre) : IRequest;
public sealed class UpdateReservationStatusHandler : IRequestHandler<UpdateReservationStatus>
{
    private readonly IUnitOfWork _uow;
    public UpdateReservationStatusHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateReservationStatus req, CancellationToken ct)
    {
        var item = await _uow.Repository<ReservationStatus>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"ReservationStatus {req.Id} not found.");
        item.Update(req.Nombre);
        await _uow.Repository<ReservationStatus>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteReservationStatus(int Id) : IRequest;
public sealed class DeleteReservationStatusHandler : IRequestHandler<DeleteReservationStatus>
{
    private readonly IUnitOfWork _uow;
    public DeleteReservationStatusHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteReservationStatus req, CancellationToken ct)
    {
        var item = await _uow.Repository<ReservationStatus>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"ReservationStatus {req.Id} not found.");
        await _uow.Repository<ReservationStatus>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}