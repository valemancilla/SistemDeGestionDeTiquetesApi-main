using FluentValidation;

namespace Application.UseCase.Flights;

public sealed class CreateFlightValidator : AbstractValidator<CreateFlight>
{
    public CreateFlightValidator()
    {
        RuleFor(x => x.CodigoVuelo)
            .NotEmpty().Length(1, 10).Matches("^[A-Za-z0-9\\-]+$");

        RuleFor(x => x.AerolineaId).GreaterThan(0);
        RuleFor(x => x.RutaId).GreaterThan(0);
        RuleFor(x => x.AeronaveId).GreaterThan(0);
        RuleFor(x => x.EstadoVueloId).GreaterThan(0);

        RuleFor(x => x.CapacidadTotal).GreaterThan(0);

        RuleFor(x => x.FechaSalida)
            .NotEmpty().GreaterThan(DateTime.UtcNow).WithMessage("La fecha de salida debe ser futura.");

        RuleFor(x => x.FechaLlegadaEstimada)
            .GreaterThan(x => x.FechaSalida)
            .WithMessage("La fecha de llegada debe ser posterior a la de salida.");
    }
}
