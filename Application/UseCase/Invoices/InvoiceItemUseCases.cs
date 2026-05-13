using Application.Abstractions;
using Domain.Entities.Invoices;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Invoices;

public sealed record GetInvoiceItems : IRequest<IReadOnlyList<InvoiceItem>>;
public sealed class GetInvoiceItemsHandler : IRequestHandler<GetInvoiceItems, IReadOnlyList<InvoiceItem>>
{
    private readonly IUnitOfWork _uow;
    public GetInvoiceItemsHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<InvoiceItem>> Handle(GetInvoiceItems _, CancellationToken ct)
        => _uow.Repository<InvoiceItem>().GetAllAsync(ct);
}

public sealed record GetInvoiceItemById(int Id) : IRequest<InvoiceItem?>;
public sealed class GetInvoiceItemByIdHandler : IRequestHandler<GetInvoiceItemById, InvoiceItem?>
{
    private readonly IUnitOfWork _uow;
    public GetInvoiceItemByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<InvoiceItem?> Handle(GetInvoiceItemById req, CancellationToken ct)
        => _uow.Repository<InvoiceItem>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateInvoiceItem(
    int FacturaId, int TipoItemId, string Descripcion,
    int Cantidad, decimal PrecioUnitario, int? ReservaPasajeroId = null) : IRequest<int>;
public sealed class CreateInvoiceItemHandler : IRequestHandler<CreateInvoiceItem, int>
{
    private readonly IUnitOfWork _uow;
    public CreateInvoiceItemHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateInvoiceItem req, CancellationToken ct)
    {
        var item = new InvoiceItem(req.FacturaId, req.TipoItemId, req.Descripcion,
            req.Cantidad, req.PrecioUnitario, req.ReservaPasajeroId);
        await _uow.Repository<InvoiceItem>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateInvoiceItemValidator : AbstractValidator<CreateInvoiceItem>
{
    public CreateInvoiceItemValidator()
    {
        RuleFor(x => x.FacturaId).GreaterThan(0);
        RuleFor(x => x.TipoItemId).GreaterThan(0);
        RuleFor(x => x.Descripcion).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Cantidad).GreaterThan(0);
        RuleFor(x => x.PrecioUnitario).GreaterThan(0);
    }
}

public sealed record UpdateInvoiceItem(int Id, int Cantidad, decimal PrecioUnitario, string Descripcion) : IRequest;
public sealed class UpdateInvoiceItemHandler : IRequestHandler<UpdateInvoiceItem>
{
    private readonly IUnitOfWork _uow;
    public UpdateInvoiceItemHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateInvoiceItem req, CancellationToken ct)
    {
        var item = await _uow.Repository<InvoiceItem>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"InvoiceItem {req.Id} not found.");
        item.Update(req.Cantidad, req.PrecioUnitario, req.Descripcion);
        await _uow.Repository<InvoiceItem>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteInvoiceItem(int Id) : IRequest;
public sealed class DeleteInvoiceItemHandler : IRequestHandler<DeleteInvoiceItem>
{
    private readonly IUnitOfWork _uow;
    public DeleteInvoiceItemHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteInvoiceItem req, CancellationToken ct)
    {
        var item = await _uow.Repository<InvoiceItem>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"InvoiceItem {req.Id} not found.");
        await _uow.Repository<InvoiceItem>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
