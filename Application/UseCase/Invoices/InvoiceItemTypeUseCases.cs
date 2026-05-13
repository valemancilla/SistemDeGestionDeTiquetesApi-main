using Application.Abstractions;
using Domain.Entities.Invoices;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Invoices;

public sealed record GetInvoiceItemTypes : IRequest<IReadOnlyList<InvoiceItemType>>;
public sealed class GetInvoiceItemTypesHandler : IRequestHandler<GetInvoiceItemTypes, IReadOnlyList<InvoiceItemType>>
{
    private readonly IUnitOfWork _uow;
    public GetInvoiceItemTypesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<InvoiceItemType>> Handle(GetInvoiceItemTypes _, CancellationToken ct)
        => _uow.Repository<InvoiceItemType>().GetAllAsync(ct);
}

public sealed record GetInvoiceItemTypeById(int Id) : IRequest<InvoiceItemType?>;
public sealed class GetInvoiceItemTypeByIdHandler : IRequestHandler<GetInvoiceItemTypeById, InvoiceItemType?>
{
    private readonly IUnitOfWork _uow;
    public GetInvoiceItemTypeByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<InvoiceItemType?> Handle(GetInvoiceItemTypeById req, CancellationToken ct)
        => _uow.Repository<InvoiceItemType>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateInvoiceItemType(string Nombre) : IRequest<int>;
public sealed class CreateInvoiceItemTypeHandler : IRequestHandler<CreateInvoiceItemType, int>
{
    private readonly IUnitOfWork _uow;
    public CreateInvoiceItemTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateInvoiceItemType req, CancellationToken ct)
    {
        var item = new InvoiceItemType(req.Nombre);
        await _uow.Repository<InvoiceItemType>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateInvoiceItemTypeValidator : AbstractValidator<CreateInvoiceItemType>
{
    public CreateInvoiceItemTypeValidator() => RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
}

public sealed record UpdateInvoiceItemType(int Id, string Nombre) : IRequest;
public sealed class UpdateInvoiceItemTypeHandler : IRequestHandler<UpdateInvoiceItemType>
{
    private readonly IUnitOfWork _uow;
    public UpdateInvoiceItemTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateInvoiceItemType req, CancellationToken ct)
    {
        var item = await _uow.Repository<InvoiceItemType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"InvoiceItemType {req.Id} not found.");
        item.Update(req.Nombre);
        await _uow.Repository<InvoiceItemType>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteInvoiceItemType(int Id) : IRequest;
public sealed class DeleteInvoiceItemTypeHandler : IRequestHandler<DeleteInvoiceItemType>
{
    private readonly IUnitOfWork _uow;
    public DeleteInvoiceItemTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteInvoiceItemType req, CancellationToken ct)
    {
        var item = await _uow.Repository<InvoiceItemType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"InvoiceItemType {req.Id} not found.");
        await _uow.Repository<InvoiceItemType>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
