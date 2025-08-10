using FluentValidation;
using Personas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Application.Validators
{
    public class PersonaUpdateValidator : AbstractValidator<PersonaUpdateDto>
    {
        public PersonaUpdateValidator()
        { 
            PersonaValidatorRules.ApplyCommonRules(this);
        }
    }
}
