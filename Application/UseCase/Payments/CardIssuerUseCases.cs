using Application.Abstractions;
using Domain.Entities.Payments;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Payments;

public sealed record GetCardIssuers : IRequest<IReadOnlyList<CardIssuer>>;
public sealed class GetCardIssuersHandler : IRequestHandler<GetCardIssuers, IReadOnlyList<CardIssuer>>
{
    private readonly IUnitOfWork _uow;
    public GetCardIssuersHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<CardIssuer>> Handle(GetCardIssuers _, CancellationToken ct)
        => _uow.Repository<CardIssuer>().GetAllAsync(ct);
}

public sealed record GetCardIssuerById(int Id) : IRequest<CardIssuer?>;
public sealed class GetCardIssuerByIdHandler : IRequestHandler<GetCardIssuerById, CardIssuer?>
{
    private readonly IUnitOfWork _uow;
    public GetCardIssuerByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<CardIssuer?> Handle(GetCardIssuerById req, CancellationToken ct)
        => _uow.Repository<CardIssuer>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateCardIssuer(string Nombre) : IRequest<int>;
public sealed class CreateCardIssuerHandler : IRequestHandler<CreateCardIssuer, int>
{
    private readonly IUnitOfWork _uow;
    public CreateCardIssuerHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateCardIssuer req, CancellationToken ct)
    {
        var item = new CardIssuer(req.Nombre);
        await _uow.Repository<CardIssuer>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateCardIssuerValidator : AbstractValidator<CreateCardIssuer>
{
    public CreateCardIssuerValidator() => RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
}

public sealed record UpdateCardIssuer(int Id, string Nombre) : IRequest;
public sealed class UpdateCardIssuerHandler : IRequestHandler<UpdateCardIssuer>
{
    private readonly IUnitOfWork _uow;
    public UpdateCardIssuerHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateCardIssuer req, CancellationToken ct)
    {
        var item = await _uow.Repository<CardIssuer>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"CardIssuer {req.Id} not found.");
        item.Update(req.Nombre);
        await _uow.Repository<CardIssuer>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteCardIssuer(int Id) : IRequest;
public sealed class DeleteCardIssuerHandler : IRequestHandler<DeleteCardIssuer>
{
    private readonly IUnitOfWork _uow;
    public DeleteCardIssuerHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteCardIssuer req, CancellationToken ct)
    {
        var item = await _uow.Repository<CardIssuer>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"CardIssuer {req.Id} not found.");
        await _uow.Repository<CardIssuer>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
