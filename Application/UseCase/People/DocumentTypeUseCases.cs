using Application.Abstractions;
using Domain.Entities.People;
using FluentValidation;
using MediatR;

namespace Application.UseCase.People;

public sealed record GetDocumentTypes : IRequest<IReadOnlyList<DocumentType>>;
public sealed class GetDocumentTypesHandler : IRequestHandler<GetDocumentTypes, IReadOnlyList<DocumentType>>
{
    private readonly IUnitOfWork _uow;
    public GetDocumentTypesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<DocumentType>> Handle(GetDocumentTypes _, CancellationToken ct)
        => _uow.Repository<DocumentType>().GetAllAsync(ct);
}

public sealed record GetDocumentTypeById(int Id) : IRequest<DocumentType?>;
public sealed class GetDocumentTypeByIdHandler : IRequestHandler<GetDocumentTypeById, DocumentType?>
{
    private readonly IUnitOfWork _uow;
    public GetDocumentTypeByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<DocumentType?> Handle(GetDocumentTypeById req, CancellationToken ct)
        => _uow.Repository<DocumentType>().GetByIdAsync(req.Id, ct);
}

public sealed record CreateDocumentType(string Nombre, string Codigo) : IRequest<int>;
public sealed class CreateDocumentTypeHandler : IRequestHandler<CreateDocumentType, int>
{
    private readonly IUnitOfWork _uow;
    public CreateDocumentTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreateDocumentType req, CancellationToken ct)
    {
        var item = new DocumentType(req.Nombre, req.Codigo);
        await _uow.Repository<DocumentType>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreateDocumentTypeValidator : AbstractValidator<CreateDocumentType>
{
    public CreateDocumentTypeValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Codigo).NotEmpty().Length(1, 255).Matches("^[A-Za-z0-9]+$");
    }
}

public sealed record UpdateDocumentType(int Id, string Nombre, string Codigo) : IRequest;
public sealed class UpdateDocumentTypeHandler : IRequestHandler<UpdateDocumentType>
{
    private readonly IUnitOfWork _uow;
    public UpdateDocumentTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdateDocumentType req, CancellationToken ct)
    {
        var item = await _uow.Repository<DocumentType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"DocumentType {req.Id} not found.");
        item.Update(req.Nombre, req.Codigo);
        await _uow.Repository<DocumentType>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
public sealed class UpdateDocumentTypeValidator : AbstractValidator<UpdateDocumentType>
{
    public UpdateDocumentTypeValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Codigo).NotEmpty().Length(1, 255).Matches("^[A-Za-z0-9]+$");
    }
}

public sealed record DeleteDocumentType(int Id) : IRequest;
public sealed class DeleteDocumentTypeHandler : IRequestHandler<DeleteDocumentType>
{
    private readonly IUnitOfWork _uow;
    public DeleteDocumentTypeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeleteDocumentType req, CancellationToken ct)
    {
        var item = await _uow.Repository<DocumentType>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"DocumentType {req.Id} not found.");
        await _uow.Repository<DocumentType>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}