using Application.Abstractions;
using Domain.Entities.Reservations;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Reservations;

public sealed record GetReservationPassengers : IRequest<IReadOnlyList<ReservationPassenger>>;
public sealed class GetReservationPassengersHandler : IRequestHandler<GetReservationPassengers, IReadOnlyList<ReservationPassenger>>
{
    private readonly IUnitOfWork _uow;
    public GetReservationPassengersHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<ReservationPassenger>> Handle(GetReservationPassengers _, CancellationToken ct)
        => _uow.Repository<ReservationPassenger>().GetAllAsync(ct);
}

public sealed record GetReservationPassengerById(int Id) : IRequest<ReservationPassenger?>;
public sealed class GetReservationPassengerByIdHandler : IRequestHandler<GetReservationPassengerById, ReservationPassenger?>
{
    private readonly IUnitOfWork _uow;
    public GetReservationPassengerByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<ReservationPassenger?> Handle(GetReservationPassengerById req, CancellationToken ct)
        => _uow.Repository<ReservationPassenger>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateReservationPassenger(int ReservaVueloId, int PasajeroId) : IRequest<int>;
public sealed class CreateReservationPassengerHandler : IRequestHandler<CreateReservationPassenger, int>
{
    private readonly IUnitOfWork _uow;
    public CreateReservationPassengerHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateReservationPassenger req, CancellationToken ct)
    {
        var item = new ReservationPassenger(req.ReservaVueloId, req.PasajeroId);
        await _uow.Repository<ReservationPassenger>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateReservationPassengerValidator : AbstractValidator<CreateReservationPassenger>
{
    public CreateReservationPassengerValidator()
    {
        RuleFor(x => x.ReservaVueloId).GreaterThan(0);
        RuleFor(x => x.PasajeroId).GreaterThan(0);
    }
}

public sealed record DeleteReservationPassenger(int Id) : IRequest;
public sealed class DeleteReservationPassengerHandler : IRequestHandler<DeleteReservationPassenger>
{
    private readonly IUnitOfWork _uow;
    public DeleteReservationPassengerHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteReservationPassenger req, CancellationToken ct)
    {
        var item = await _uow.Repository<ReservationPassenger>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"ReservationPassenger {req.Id} not found.");
        await _uow.Repository<ReservationPassenger>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}