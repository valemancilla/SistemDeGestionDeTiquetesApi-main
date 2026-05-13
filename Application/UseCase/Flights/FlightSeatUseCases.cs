using Application.Abstractions;
using Domain.Entities.Flights;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Flights;

public sealed record GetFlightSeats : IRequest<IReadOnlyList<FlightSeat>>;
public sealed class GetFlightSeatsHandler : IRequestHandler<GetFlightSeats, IReadOnlyList<FlightSeat>>
{
    private readonly IUnitOfWork _uow;
    public GetFlightSeatsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<FlightSeat>> Handle(GetFlightSeats _, CancellationToken ct)
        => _uow.Repository<FlightSeat>().GetAllAsync(ct);
}

public sealed record GetFlightSeatById(int Id) : IRequest<FlightSeat?>;
public sealed class GetFlightSeatByIdHandler : IRequestHandler<GetFlightSeatById, FlightSeat?>
{
    private readonly IUnitOfWork _uow;
    public GetFlightSeatByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<FlightSeat?> Handle(GetFlightSeatById req, CancellationToken ct)
        => _uow.Repository<FlightSeat>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateFlightSeat(int VueloId, string CodigoAsiento, int TipoCabinaId, int TipoUbicacionId) : IRequest<int>;
public sealed class CreateFlightSeatHandler : IRequestHandler<CreateFlightSeat, int>
{
    private readonly IUnitOfWork _uow;
    public CreateFlightSeatHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateFlightSeat req, CancellationToken ct)
    {
        var item = new FlightSeat(req.VueloId, req.CodigoAsiento, req.TipoCabinaId, req.TipoUbicacionId);
        await _uow.Repository<FlightSeat>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateFlightSeatValidator : AbstractValidator<CreateFlightSeat>
{
    public CreateFlightSeatValidator()
    {
        RuleFor(x => x.VueloId).GreaterThan(0);
        RuleFor(x => x.CodigoAsiento).NotEmpty().Length(1, 5).Matches("^[A-Za-z0-9]+$");
        RuleFor(x => x.TipoCabinaId).GreaterThan(0);
        RuleFor(x => x.TipoUbicacionId).GreaterThan(0);
    }
}

public sealed record DeleteFlightSeat(int Id) : IRequest;
public sealed class DeleteFlightSeatHandler : IRequestHandler<DeleteFlightSeat>
{
    private readonly IUnitOfWork _uow;
    public DeleteFlightSeatHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteFlightSeat req, CancellationToken ct)
    {
        var item = await _uow.Repository<FlightSeat>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"FlightSeat {req.Id} not found.");
        await _uow.Repository<FlightSeat>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}