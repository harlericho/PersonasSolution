using AutoMapper;
using Personas.Application.DTOs;
using Personas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Application.Mappings
{
    public class PersonaProfile : Profile
    {
        public PersonaProfile() 
        {
            CreateMap<Persona, PersonaDto>().ReverseMap();
            CreateMap<Persona, PersonaCreateDto>().ReverseMap();
            CreateMap<Persona, PersonaUpdateDto>().ReverseMap();
        }
    }
}
