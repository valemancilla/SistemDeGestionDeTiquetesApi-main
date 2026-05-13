using Application.Abstractions;
using Domain.Entities.People;
using FluentValidation;
using MediatR;

namespace Application.UseCase.People;

public sealed record GetPhoneCodes : IRequest<IReadOnlyList<PhoneCode>>;
public sealed class GetPhoneCodesHandler : IRequestHandler<GetPhoneCodes, IReadOnlyList<PhoneCode>>
{
    private readonly IUnitOfWork _uow;
    public GetPhoneCodesHandler(IUnitOfWork uow) => _uow = uow;
    public Task<IReadOnlyList<PhoneCode>> Handle(GetPhoneCodes _, CancellationToken ct)
        => _uow.Repository<PhoneCode>().GetAllAsync(ct);
}

public sealed record GetPhoneCodeById(int Id) : IRequest<PhoneCode?>;
public sealed class GetPhoneCodeByIdHandler : IRequestHandler<GetPhoneCodeById, PhoneCode?>
{
    private readonly IUnitOfWork _uow;
    public GetPhoneCodeByIdHandler(IUnitOfWork uow) => _uow = uow;
    public Task<PhoneCode?> Handle(GetPhoneCodeById req, CancellationToken ct)
        => _uow.Repository<PhoneCode>().GetByIdAsync(req.Id, ct);
}

public sealed record CreatePhoneCode(string CodigoPais, string NombrePais) : IRequest<int>;
public sealed class CreatePhoneCodeHandler : IRequestHandler<CreatePhoneCode, int>
{
    private readonly IUnitOfWork _uow;
    public CreatePhoneCodeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task<int> Handle(CreatePhoneCode req, CancellationToken ct)
    {
        var item = new PhoneCode(req.CodigoPais, req.NombrePais);
        await _uow.Repository<PhoneCode>().AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }
}
public sealed class CreatePhoneCodeValidator : AbstractValidator<CreatePhoneCode>
{
    public CreatePhoneCodeValidator()
    {
        RuleFor(x => x.CodigoPais).NotEmpty().Length(1, 5).Matches(@"^\+?[0-9]{1,4}$");
        RuleFor(x => x.NombrePais).NotEmpty().MaximumLength(100);
    }
}

public sealed record UpdatePhoneCode(int Id, string CodigoPais, string NombrePais) : IRequest;
public sealed class UpdatePhoneCodeHandler : IRequestHandler<UpdatePhoneCode>
{
    private readonly IUnitOfWork _uow;
    public UpdatePhoneCodeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(UpdatePhoneCode req, CancellationToken ct)
    {
        var item = await _uow.Repository<PhoneCode>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"PhoneCode {req.Id} not found.");
        item.Update(req.CodigoPais, req.NombrePais);
        await _uow.Repository<PhoneCode>().UpdateAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}
public sealed class UpdatePhoneCodeValidator : AbstractValidator<UpdatePhoneCode>
{
    public UpdatePhoneCodeValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.CodigoPais).NotEmpty().Length(1, 5).Matches(@"^\+?[0-9]{1,4}$");
        RuleFor(x => x.NombrePais).NotEmpty().MaximumLength(100);
    }
}

public sealed record DeletePhoneCode(int Id) : IRequest;
public sealed class DeletePhoneCodeHandler : IRequestHandler<DeletePhoneCode>
{
    private readonly IUnitOfWork _uow;
    public DeletePhoneCodeHandler(IUnitOfWork uow) => _uow = uow;
    public async Task Handle(DeletePhoneCode req, CancellationToken ct)
    {
        var item = await _uow.Repository<PhoneCode>().GetByIdAsync(req.Id, ct)
            ?? throw new KeyNotFoundException($"PhoneCode {req.Id} not found.");
        await _uow.Repository<PhoneCode>().RemoveAsync(item, ct);
        await _uow.SaveChangesAsync(ct);
    }
}