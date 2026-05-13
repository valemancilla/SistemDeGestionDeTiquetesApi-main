using Application.Abstractions;
using Domain.Entities.Payments;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Payments;

public sealed record GetPaymentMethodTypes : IRequest<IReadOnlyList<PaymentMethodType>>;
public sealed class GetPaymentMethodTypesHandler : IRequestHandler<GetPaymentMethodTypes, IReadOnlyList<PaymentMethodType>>
{
    private readonly IUnitOfWork _uow;
    public GetPaymentMethodTypesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<PaymentMethodType>> Handle(GetPaymentMethodTypes _, CancellationToken ct)
        => _uow.Repository<PaymentMethodType>().GetAllAsync(ct);
}

public sealed record GetPaymentMethodTypeById(int Id) : IRequest<PaymentMethodType?>;
public sealed class GetPaymentMethodTypeByIdHandler : IRequestHandler<GetPaymentMethodTypeById, PaymentMethodType?>
{
    private readonly IUnitOfWork _uow;
    public GetPaymentMethodTypeByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<PaymentMethodType?> Handle(GetPaymentMethodTypeById req, CancellationToken ct)
        => _uow.Repository<PaymentMethodType>().GetByIdAsync(req.Id, ct);
}

public sealed record CreatePaymentMethodType(string Nombre) : IRequest<int>;
public sealed class CreatePaymentMethodTypeHandler : IRequestHandler<CreatePaymentMethodType, int>
{
    private readonly IUnitOfWork _uow;
    public CreatePaymentMethodTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreatePaymentMethodType req, CancellationToken ct)
    {
        var item = new PaymentMethodType(req.Nombre);
        await _uow.Repository<PaymentMethodType>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreatePaymentMethodTypeValidator : AbstractValidator<CreatePaymentMethodType>
{
    public CreatePaymentMethodTypeValidator() => RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
}

public sealed record UpdatePaymentMethodType(int Id, string Nombre) : IRequest;
public sealed class UpdatePaymentMethodTypeHandler : IRequestHandler<UpdatePaymentMethodType>
{
    private readonly IUnitOfWork _uow;
    public UpdatePaymentMethodTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdatePaymentMethodType req, CancellationToken ct)
    {
        var item = await _uow.Repository<PaymentMethodType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"PaymentMethodType {req.Id} not found.");
        item.Update(req.Nombre);
        await _uow.Repository<PaymentMethodType>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeletePaymentMethodType(int Id) : IRequest;
public sealed class DeletePaymentMethodTypeHandler : IRequestHandler<DeletePaymentMethodType>
{
    private readonly IUnitOfWork _uow;
    public DeletePaymentMethodTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeletePaymentMethodType req, CancellationToken ct)
    {
        var item = await _uow.Repository<PaymentMethodType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"PaymentMethodType {req.Id} not found.");
        await _uow.Repository<PaymentMethodType>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
