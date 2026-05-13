using Application.Abstractions;
using Domain.Entities.Payments;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Payments;

public sealed record GetCardTypes : IRequest<IReadOnlyList<CardType>>;
public sealed class GetCardTypesHandler : IRequestHandler<GetCardTypes, IReadOnlyList<CardType>>
{
    private readonly IUnitOfWork _uow;
    public GetCardTypesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<CardType>> Handle(GetCardTypes _, CancellationToken ct)
        => _uow.Repository<CardType>().GetAllAsync(ct);
}

public sealed record GetCardTypeById(int Id) : IRequest<CardType?>;
public sealed class GetCardTypeByIdHandler : IRequestHandler<GetCardTypeById, CardType?>
{
    private readonly IUnitOfWork _uow;
    public GetCardTypeByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<CardType?> Handle(GetCardTypeById req, CancellationToken ct)
        => _uow.Repository<CardType>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateCardType(string Nombre) : IRequest<int>;
public sealed class CreateCardTypeHandler : IRequestHandler<CreateCardType, int>
{
    private readonly IUnitOfWork _uow;
    public CreateCardTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateCardType req, CancellationToken ct)
    {
        var item = new CardType(req.Nombre);
        await _uow.Repository<CardType>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateCardTypeValidator : AbstractValidator<CreateCardType>
{
    public CreateCardTypeValidator() => RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
}

public sealed record UpdateCardType(int Id, string Nombre) : IRequest;
public sealed class UpdateCardTypeHandler : IRequestHandler<UpdateCardType>
{
    private readonly IUnitOfWork _uow;
    public UpdateCardTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateCardType req, CancellationToken ct)
    {
        var item = await _uow.Repository<CardType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"CardType {req.Id} not found.");
        item.Update(req.Nombre);
        await _uow.Repository<CardType>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteCardType(int Id) : IRequest;
public sealed class DeleteCardTypeHandler : IRequestHandler<DeleteCardType>
{
    private readonly IUnitOfWork _uow;
    public DeleteCardTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteCardType req, CancellationToken ct)
    {
        var item = await _uow.Repository<CardType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"CardType {req.Id} not found.");
        await _uow.Repository<CardType>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
