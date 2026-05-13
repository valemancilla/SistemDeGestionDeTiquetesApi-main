using Application.Abstractions;
using Domain.Entities.Reservations;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Reservations;

public sealed record GetReservationFlights : IRequest<IReadOnlyList<ReservationFlight>>;
public sealed class GetReservationFlightsHandler : IRequestHandler<GetReservationFlights, IReadOnlyList<ReservationFlight>>
{
    private readonly IUnitOfWork _uow;
    public GetReservationFlightsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<ReservationFlight>> Handle(GetReservationFlights _, CancellationToken ct)
        => _uow.Repository<ReservationFlight>().GetAllAsync(ct);
}

public sealed record GetReservationFlightById(int Id) : IRequest<ReservationFlight?>;
public sealed class GetReservationFlightByIdHandler : IRequestHandler<GetReservationFlightById, ReservationFlight?>
{
    private readonly IUnitOfWork _uow;
    public GetReservationFlightByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<ReservationFlight?> Handle(GetReservationFlightById req, CancellationToken ct)
        => _uow.Repository<ReservationFlight>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateReservationFlight(int ReservaId, int VueloId, decimal ValorParcial) : IRequest<int>;
public sealed class CreateReservationFlightHandler : IRequestHandler<CreateReservationFlight, int>
{
    private readonly IUnitOfWork _uow;
    public CreateReservationFlightHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateReservationFlight req, CancellationToken ct)
    {
        var item = new ReservationFlight(req.ReservaId, req.VueloId, req.ValorParcial);
        await _uow.Repository<ReservationFlight>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateReservationFlightValidator : AbstractValidator<CreateReservationFlight>
{
    public CreateReservationFlightValidator()
    {
        RuleFor(x => x.ReservaId).GreaterThan(0);
        RuleFor(x => x.VueloId).GreaterThan(0);
        RuleFor(x => x.ValorParcial).GreaterThanOrEqualTo(0);
    }
}

public sealed record UpdateReservationFlight(int Id, decimal ValorParcial) : IRequest;
public sealed class UpdateReservationFlightHandler : IRequestHandler<UpdateReservationFlight>
{
    private readonly IUnitOfWork _uow;
    public UpdateReservationFlightHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateReservationFlight req, CancellationToken ct)
    {
        var item = await _uow.Repository<ReservationFlight>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"ReservationFlight {req.Id} not found.");
        item.Update(req.ValorParcial);
        await _uow.Repository<ReservationFlight>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteReservationFlight(int Id) : IRequest;
public sealed class DeleteReservationFlightHandler : IRequestHandler<DeleteReservationFlight>
{
    private readonly IUnitOfWork _uow;
    public DeleteReservationFlightHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteReservationFlight req, CancellationToken ct)
    {
        var item = await _uow.Repository<ReservationFlight>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"ReservationFlight {req.Id} not found.");
        await _uow.Repository<ReservationFlight>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}