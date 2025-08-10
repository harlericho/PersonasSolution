using AutoMapper;
using FluentValidation;
using Personas.Application.DTOs;
using Personas.Domain.Entities;
using Personas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Application.Services
{
    public class PersonaService
    {
        private readonly IRepository<Persona> _personaRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<PersonaCreateDto> _createdvalidator;
        private readonly IValidator<PersonaUpdateDto> _updatedvalidator;
        public PersonaService(IRepository<Persona> personaRepository, IMapper mapper,
            IValidator<PersonaCreateDto> createdValidator,
            IValidator<PersonaUpdateDto> updatedValidator)
        {
            _personaRepository = personaRepository ?? throw new ArgumentNullException(nameof(personaRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _createdvalidator = createdValidator ?? throw new ArgumentNullException(nameof(createdValidator));
            _updatedvalidator = updatedValidator ?? throw new ArgumentNullException(nameof(updatedValidator));
        }
        public async Task<IEnumerable<PersonaDto>> GetAllPersonasAsync()
        {
            var personas = await _personaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PersonaDto>>(personas);
        }
        public async Task<PersonaDto> GetPersonaByIdAsync(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);
            if (persona == null)
            {
                return null;
            }
            return _mapper.Map<PersonaDto>(persona);
        }
        public async Task AddPersonaAsync(PersonaCreateDto personaDto)
        {
            var result = await _createdvalidator.ValidateAsync(personaDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var persona = _mapper.Map<Persona>(personaDto);
            await _personaRepository.AddAsync(persona);
        }
        public async Task UpdatePersonaAsync(PersonaUpdateDto personaDto)
        {
            var result = await _updatedvalidator.ValidateAsync(personaDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var persona = _mapper.Map<Persona>(personaDto);
            await _personaRepository.UpdateAsync(persona);
        }
        public async Task DeletePersonaAsync(int id)
        {
            await _personaRepository.DeleteAsync(id);
        }
    }
}
