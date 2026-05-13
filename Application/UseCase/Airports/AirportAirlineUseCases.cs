using Application.Abstractions;
using Domain.Entities.Airports;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Airports;

public sealed record GetAirportAirlines : IRequest<IReadOnlyList<AirportAirline>>;
public sealed class GetAirportAirlinesHandler : IRequestHandler<GetAirportAirlines, IReadOnlyList<AirportAirline>>
{
    private readonly IUnitOfWork _uow;
    public GetAirportAirlinesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<AirportAirline>> Handle(GetAirportAirlines _, CancellationToken ct)
        => _uow.Repository<AirportAirline>().GetAllAsync(ct);
}

public sealed record GetAirportAirlineById(int Id) : IRequest<AirportAirline?>;
public sealed class GetAirportAirlineByIdHandler : IRequestHandler<GetAirportAirlineById, AirportAirline?>
{
    private readonly IUnitOfWork _uow;
    public GetAirportAirlineByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<AirportAirline?> Handle(GetAirportAirlineById req, CancellationToken ct)
        => _uow.Repository<AirportAirline>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateAirportAirline(
    int AeropuertoId, int AerolineaId, DateOnly FechaInicio,
    string? Terminal, DateOnly? FechaFin) : IRequest<int>;
public sealed class CreateAirportAirlineHandler : IRequestHandler<CreateAirportAirline, int>
{
    private readonly IUnitOfWork _uow;
    public CreateAirportAirlineHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateAirportAirline req, CancellationToken ct)
    {
        var item = new AirportAirline(req.AeropuertoId, req.AerolineaId, req.FechaInicio, req.Terminal, req.FechaFin);
        await _uow.Repository<AirportAirline>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateAirportAirlineValidator : AbstractValidator<CreateAirportAirline>
{
    public CreateAirportAirlineValidator()
    {
        RuleFor(x => x.AeropuertoId).GreaterThan(0);
        RuleFor(x => x.AerolineaId).GreaterThan(0);
        RuleFor(x => x.Terminal)
            .Length(1, 20)
            .Matches("^[A-Za-z0-9 .\\-]+$")
            .When(x => !string.IsNullOrWhiteSpace(x.Terminal));
    }
}

public sealed record UpdateAirportAirline(int Id, string? Terminal, DateOnly? FechaFin, bool Activa) : IRequest;
public sealed class UpdateAirportAirlineHandler : IRequestHandler<UpdateAirportAirline>
{
    private readonly IUnitOfWork _uow;
    public UpdateAirportAirlineHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateAirportAirline req, CancellationToken ct)
    {
        var item = await _uow.Repository<AirportAirline>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"AirportAirline {req.Id} not found.");
        item.Update(req.Terminal, req.FechaFin, req.Activa);
        await _uow.Repository<AirportAirline>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed class UpdateAirportAirlineValidator : AbstractValidator<UpdateAirportAirline>
{
    public UpdateAirportAirlineValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Terminal)
            .Length(1, 20)
            .Matches("^[A-Za-z0-9 .\\-]+$")
            .When(x => !string.IsNullOrWhiteSpace(x.Terminal));
    }
}

public sealed record DeleteAirportAirline(int Id) : IRequest;
public sealed class DeleteAirportAirlineHandler : IRequestHandler<DeleteAirportAirline>
{
    private readonly IUnitOfWork _uow;
    public DeleteAirportAirlineHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteAirportAirline req, CancellationToken ct)
    {
        var item = await _uow.Repository<AirportAirline>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"AirportAirline {req.Id} not found.");
        await _uow.Repository<AirportAirline>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}