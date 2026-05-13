using FluentValidation;

namespace Application.UseCase.Airlines;

public sealed class CreateAirlineValidator : AbstractValidator<CreateAirline>
{
    public CreateAirlineValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(150);
        RuleFor(x => x.CodigoIata).NotEmpty().Length(3, 3).Matches("^[A-Za-z]{3}$");
        RuleFor(x => x.PaisOrigenId).GreaterThan(0);
    }
}
