using FluentValidation;
using Personas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Application.Validators
{
    public class PersonaCreateValidator : AbstractValidator<PersonaCreateDto>
    {
        public PersonaCreateValidator()
        {
            PersonaValidatorRules.ApplyCommonRules(this);
        }
    }
}
