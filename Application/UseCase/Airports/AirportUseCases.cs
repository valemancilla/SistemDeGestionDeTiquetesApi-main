using Application.Abstractions;
using Domain.Entities.Airports;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Airports;

public sealed record GetAirports : IRequest<IReadOnlyList<Airport>>;
public sealed class GetAirportsHandler : IRequestHandler<GetAirports, IReadOnlyList<Airport>>
{
    private readonly IUnitOfWork _uow;
    public GetAirportsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<Airport>> Handle(GetAirports _, CancellationToken ct)
        => _uow.Repository<Airport>().GetAllAsync(ct);
}

public sealed record GetAirportById(int Id) : IRequest<Airport?>;
public sealed class GetAirportByIdHandler : IRequestHandler<GetAirportById, Airport?>
{
    private readonly IUnitOfWork _uow;
    public GetAirportByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<Airport?> Handle(GetAirportById req, CancellationToken ct)
        => _uow.Repository<Airport>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateAirport(string Nombre, string CodigoIata, int CiudadId, string? CodigoIcao) : IRequest<int>;
public sealed class CreateAirportHandler : IRequestHandler<CreateAirport, int>
{
    private readonly IUnitOfWork _uow;
    public CreateAirportHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateAirport req, CancellationToken ct)
    {
        var item = new Airport(req.Nombre, req.CodigoIata, req.CiudadId, req.CodigoIcao);
        await _uow.Repository<Airport>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateAirportValidator : AbstractValidator<CreateAirport>
{
    public CreateAirportValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(150);
        RuleFor(x => x.CodigoIata).NotEmpty().Length(3, 3).Matches("^[A-Za-z]{3}$");
        RuleFor(x => x.CodigoIcao)
            .Length(4, 4)
            .Matches("^[A-Za-z]{4}$")
            .When(x => !string.IsNullOrWhiteSpace(x.CodigoIcao));
        RuleFor(x => x.CiudadId).GreaterThan(0);
    }
}

public sealed record UpdateAirport(int Id, string Nombre, string? CodigoIcao) : IRequest;
public sealed class UpdateAirportValidator : AbstractValidator<UpdateAirport>
{
    public UpdateAirportValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(150);
        RuleFor(x => x.CodigoIcao)
            .Length(4, 4)
            .Matches("^[A-Za-z]{4}$")
            .When(x => !string.IsNullOrWhiteSpace(x.CodigoIcao));
    }
}

public sealed class UpdateAirportHandler : IRequestHandler<UpdateAirport>
{
    private readonly IUnitOfWork _uow;
    public UpdateAirportHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateAirport req, CancellationToken ct)
    {
        var item = await _uow.Repository<Airport>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Airport {req.Id} not found.");
        item.Update(req.Nombre, req.CodigoIcao);
        await _uow.Repository<Airport>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteAirport(int Id) : IRequest;
public sealed class DeleteAirportHandler : IRequestHandler<DeleteAirport>
{
    private readonly IUnitOfWork _uow;
    public DeleteAirportHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteAirport req, CancellationToken ct)
    {
        var item = await _uow.Repository<Airport>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Airport {req.Id} not found.");
        await _uow.Repository<Airport>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}