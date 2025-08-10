using FluentValidation;
using Personas.Application.DTOs;

namespace Personas.Application.Validators
{
    public static class PersonaValidatorRules
    {
        public static void ApplyCommonRules<T>(AbstractValidator<T> validator) where T : IPersonaBase
        {
            validator.RuleFor(x => x.Cedula)
               .NotEmpty().WithMessage("La cédula es obligatoria")
               .Matches(@"^\d{10}$").WithMessage("La cédula debe tener exactamente 10 dígitos numéricos");

            validator.RuleFor(x => x.Nombres)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(50);

            validator.RuleFor(x => x.Edad)
                .InclusiveBetween(0, 120).WithMessage("La edad debe estar entre 0 y 120 años");
        }
    }
}
