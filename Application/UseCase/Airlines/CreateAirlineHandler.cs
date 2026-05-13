using Application.Abstractions;
using Domain.Entities.Airlines;
using MediatR;

namespace Application.UseCase.Airlines;

public sealed class CreateAirlineHandler : IRequestHandler<CreateAirline, int>
{
    private readonly IUnitOfWork _uow;

    public CreateAirlineHandler(IUnitOfWork uow) => _uow = uow;

    public async Task<int> Handle(CreateAirline req, CancellationToken ct)
    {
        if (await _uow.Airlines.ExistsCodigoIataAsync(req.CodigoIata, ct))
            throw new InvalidOperationException($"Ya existe una aerolínea con código IATA '{req.CodigoIata}'.");

        var airline = new Airline(req.Nombre, req.CodigoIata, req.PaisOrigenId);
        await _uow.Airlines.AddAsync(airline, ct);
        await _uow.SaveChangesAsync(ct);
        return airline.Id;
    }
}
