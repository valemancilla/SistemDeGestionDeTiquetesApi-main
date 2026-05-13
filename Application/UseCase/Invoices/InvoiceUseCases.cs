using Application.Abstractions;
using Domain.Entities.Invoices;
using FluentValidation;
using MediatR;

namespace Application.UseCase.Invoices;

public sealed record GetInvoices : IRequest<IReadOnlyList<Invoice>>;
public sealed class GetInvoicesHandler : IRequestHandler<GetInvoices, IReadOnlyList<Invoice>>
{
    private readonly IUnitOfWork _uow;
    public GetInvoicesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<Invoice>> Handle(GetInvoices _, CancellationToken ct)
        => _uow.Invoices.GetAllAsync(ct);
}

public sealed record GetInvoiceById(int Id) : IRequest<Invoice?>;
public sealed class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceById, Invoice?>
{
    private readonly IUnitOfWork _uow;
    public GetInvoiceByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<Invoice?> Handle(GetInvoiceById req, CancellationToken ct)
        => _uow.Invoices.GetByIdAsync(req.Id, ct);
}

public sealed record CreateInvoice(int ReservaId, string NumeroFactura, decimal Subtotal, decimal Impuestos) : IRequest<int>;
public sealed class CreateInvoiceHandler : IRequestHandler<CreateInvoice, int>
{
    private readonly IUnitOfWork _uow;
    public CreateInvoiceHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateInvoice req, CancellationToken ct)
    {
        var item = new Invoice(req.ReservaId, req.NumeroFactura, req.Subtotal, req.Impuestos);
        await _uow.Invoices.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateInvoiceValidator : AbstractValidator<CreateInvoice>
{
    public CreateInvoiceValidator()
    {
        RuleFor(x => x.ReservaId).GreaterThan(0);
        RuleFor(x => x.NumeroFactura).NotEmpty().Length(1, 30).Matches("^[A-Za-z0-9\\-]+$");
        RuleFor(x => x.Subtotal).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Impuestos).GreaterThanOrEqualTo(0);
    }
}

public sealed record UpdateInvoice(int Id, decimal Subtotal, decimal Impuestos) : IRequest;
public sealed class UpdateInvoiceHandler : IRequestHandler<UpdateInvoice>
{
    private readonly IUnitOfWork _uow;
    public UpdateInvoiceHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateInvoice req, CancellationToken ct)
    {
        var item = await _uow.Invoices.GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Invoice {req.Id} not found.");
        item.RecalcularTotal(req.Subtotal, req.Impuestos);
        await _uow.Invoices.UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}

public sealed record DeleteInvoice(int Id) : IRequest;
public sealed class DeleteInvoiceHandler : IRequestHandler<DeleteInvoice>
{
    private readonly IUnitOfWork _uow;
    public DeleteInvoiceHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteInvoice req, CancellationToken ct)
    {
        var item = await _uow.Invoices.GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"Invoice {req.Id} not found.");
        await _uow.Invoices.RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
