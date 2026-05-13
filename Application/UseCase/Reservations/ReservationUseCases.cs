using Application.Abstractions;
using Domain.Entities.Reservations;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Reservations;

public sealed record GetReservations : IRequest<IReadOnlyList<Reservation>>;
public sealed class GetReservationsHandler : IRequestHandler<GetReservations, IReadOnlyList<Reservation>>
{
    private readonly IUnitOfWork _uow;
    public GetReservationsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<Reservation>> Handle(GetReservations _, CancellationToken ct)
        => _uow.Reservations.GetAllAsync(ct);
}

public sealed record GetReservationById(int Id) : IRequest<Reservation?>;
public sealed class GetReservationByIdHandler : IRequestHandler<GetReservationById, Reservation?>
{
    private readonly IUnitOfWork _uow;
    public GetReservationByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<Reservation?> Handle(GetReservationById req, CancellationToken ct)
        => _uow.Reservations.GetByIdAsync(req.Id, ct);
}

public sealed record CreateReservation(
    string CodigoReserva, int ClienteId, int EstadoReservaId,
    decimal ValorTotal, DateTime? VenceEn) : IRequest<int>;
public sealed class CreateReservationHandler : IRequestHandler<CreateReservation, int>
{
    private readonly IUnitOfWork _uow;
    public CreateReservationHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateReservation req, CancellationToken ct)
    {
        var item = new Reservation(req.CodigoReserva, req.ClienteId, req.EstadoReservaId, req.ValorTotal, req.VenceEn);
        await _uow.Reservations.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateReservationValidator : AbstractValidator<CreateReservation>
{
    public CreateReservationValidator()
    {
        RuleFor(x => x.CodigoReserva).NotEmpty().Length(1, 30).Matches("^[A-Za-z0-9]+$");
        RuleFor(x => x.ClienteId).GreaterThan(0);
        RuleFor(x => x.EstadoReservaId).GreaterThan(0);
        RuleFor(x => x.ValorTotal).GreaterThanOrEqualTo(0);
    }
}

public sealed record UpdateReservation(int Id, decimal ValorTotal, DateTime? VenceEn) : IRequest;
public sealed class UpdateReservationHandler : IRequestHandler<UpdateReservation>
{
    private readonly IUnitOfWork _uow;
    public UpdateReservationHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateReservation req, CancellationToken ct)
    {
        var item = await _uow.Reservations.GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Reservation {req.Id} not found.");
        item.Update(req.ValorTotal, req.VenceEn);
        await _uow.Reservations.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record ChangeReservationStatus(int Id, int NuevoEstadoId) : IRequest;
public sealed class ChangeReservationStatusHandler : IRequestHandler<ChangeReservationStatus>
{
    private readonly IUnitOfWork _uow;
    public ChangeReservationStatusHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(ChangeReservationStatus req, CancellationToken ct)
    {
        var item = await _uow.Reservations.GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Reservation {req.Id} not found.");
        item.CambiarEstado(req.NuevoEstadoId);
        await _uow.Reservations.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}