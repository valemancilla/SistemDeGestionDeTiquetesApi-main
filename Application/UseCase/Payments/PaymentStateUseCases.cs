using Application.Abstractions;
using Domain.Entities.Payments;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Payments;

public sealed record GetPaymentStates : IRequest<IReadOnlyList<PaymentState>>;
public sealed class GetPaymentStatesHandler : IRequestHandler<GetPaymentStates, IReadOnlyList<PaymentState>>
{
    private readonly IUnitOfWork _uow;
    public GetPaymentStatesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<PaymentState>> Handle(GetPaymentStates _, CancellationToken ct)
        => _uow.Repository<PaymentState>().GetAllAsync(ct);
}

public sealed record GetPaymentStateById(int Id) : IRequest<PaymentState?>;
public sealed class GetPaymentStateByIdHandler : IRequestHandler<GetPaymentStateById, PaymentState?>
{
    private readonly IUnitOfWork _uow;
    public GetPaymentStateByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<PaymentState?> Handle(GetPaymentStateById req, CancellationToken ct)
        => _uow.Repository<PaymentState>().GetByIdAsync(req.Id, ct);
}

public sealed record CreatePaymentState(string Nombre) : IRequest<int>;
public sealed class CreatePaymentStateHandler : IRequestHandler<CreatePaymentState, int>
{
    private readonly IUnitOfWork _uow;
    public CreatePaymentStateHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreatePaymentState req, CancellationToken ct)
    {
        var item = new PaymentState(req.Nombre);
        await _uow.Repository<PaymentState>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreatePaymentStateValidator : AbstractValidator<CreatePaymentState>
{
    public CreatePaymentStateValidator() => RuleFor(x => x.Nombre).NotEmpty().MaximumLength(PaymentStates.NombreMaxLength);
}

public sealed record UpdatePaymentState(int Id, string Nombre) : IRequest;
public sealed class UpdatePaymentStateHandler : IRequestHandler<UpdatePaymentState>
{
    private readonly IUnitOfWork _uow;
    public UpdatePaymentStateHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdatePaymentState req, CancellationToken ct)
    {
        var item = await _uow.Repository<PaymentState>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"PaymentState {req.Id} not found.");
        item.Update(req.Nombre);
        await _uow.Repository<PaymentState>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeletePaymentState(int Id) : IRequest;
public sealed class DeletePaymentStateHandler : IRequestHandler<DeletePaymentState>
{
    private readonly IUnitOfWork _uow;
    public DeletePaymentStateHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeletePaymentState req, CancellationToken ct)
    {
        var item = await _uow.Repository<PaymentState>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"PaymentState {req.Id} not found.");
        await _uow.Repository<PaymentState>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
