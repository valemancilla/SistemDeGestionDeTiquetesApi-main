using Application.Abstractions;
using Domain.Entities.Flights;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Flights;

public sealed record GetFlightHistories : IRequest<IReadOnlyList<FlightHistory>>;
public sealed class GetFlightHistoriesHandler : IRequestHandler<GetFlightHistories, IReadOnlyList<FlightHistory>>
{
    private readonly IUnitOfWork _uow;
    public GetFlightHistoriesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<FlightHistory>> Handle(GetFlightHistories _, CancellationToken ct)
        => _uow.Repository<FlightHistory>().GetAllAsync(ct);
}

public sealed record GetFlightHistoryById(int Id) : IRequest<FlightHistory?>;
public sealed class GetFlightHistoryByIdHandler : IRequestHandler<GetFlightHistoryById, FlightHistory?>
{
    private readonly IUnitOfWork _uow;
    public GetFlightHistoryByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<FlightHistory?> Handle(GetFlightHistoryById req, CancellationToken ct)
        => _uow.Repository<FlightHistory>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateFlightHistory(
    int VueloId, int EstadoAnteriorId, int EstadoNuevoId,
    int? CambiadoPor, string? Observacion) : IRequest<int>;
public sealed class CreateFlightHistoryHandler : IRequestHandler<CreateFlightHistory, int>
{
    private readonly IUnitOfWork _uow;
    public CreateFlightHistoryHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateFlightHistory req, CancellationToken ct)
    {
        var item = new FlightHistory(req.VueloId, req.EstadoAnteriorId, req.EstadoNuevoId, req.CambiadoPor, req.Observacion);
        await _uow.Repository<FlightHistory>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateFlightHistoryValidator : AbstractValidator<CreateFlightHistory>
{
    public CreateFlightHistoryValidator()
    {
        RuleFor(x => x.VueloId).GreaterThan(0);
        RuleFor(x => x.EstadoAnteriorId).GreaterThan(0);
        RuleFor(x => x.EstadoNuevoId).GreaterThan(0);
    }
}