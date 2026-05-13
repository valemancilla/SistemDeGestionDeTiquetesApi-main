using Application.Abstractions;
using Domain.Entities.Airlines;
using MediatR;

namespace Application.UseCase.Airlines;

public sealed class GetAirlinesHandler : IRequestHandler<GetAirlines, IReadOnlyList<Airline>>
{
    private readonly IUnitOfWork _uow;

    public GetAirlinesHandler(IUnitOfWork uow) => _uow = uow;

    public Task<IReadOnlyList<Airline>> Handle(GetAirlines request, CancellationToken ct)
        => request.SoloActivas
            ? _uow.Airlines.GetActivasAsync(ct)
            : _uow.Airlines.GetAllAsync(ct);
}
