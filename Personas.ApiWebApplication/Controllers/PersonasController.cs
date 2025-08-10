using Microsoft.AspNetCore.Mvc;
using Personas.Application.DTOs;
using Personas.Application.Services;

namespace Personas.ApiWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : Controller
    {
        private readonly PersonaService _personaService;
        private readonly ILogger<PersonasController> _logger;

        public PersonasController(PersonaService personaService, ILogger<PersonasController> logger)
        {
            _personaService = personaService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPersonas()
        {
            try
            {
                var personas = await _personaService.GetAllPersonasAsync();
                return Ok(personas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving personas");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonaById(int id)
        {
            try
            {
                var persona = await _personaService.GetPersonaByIdAsync(id);
                if (persona == null)
                {
                    return NotFound();
                }
                return Ok(persona);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving persona with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddPersona([FromBody] PersonaCreateDto personaDto)
        {
            if (personaDto == null)
            {
                return BadRequest("Persona data is null");
            }
            try
            {
                await _personaService.AddPersonaAsync(personaDto);
                return Ok("Persona added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding persona");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePersona([FromBody] PersonaUpdateDto personaDto)
        {
            if (personaDto == null)
            {
                return BadRequest("Persona data is null");
            }
            try
            {
                await _personaService.UpdatePersonaAsync(personaDto);
                return Ok("Persona updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating persona with ID {Id}", personaDto.Id);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            try
            {
                await _personaService.DeletePersonaAsync(id);
                return Ok("Persona deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting persona with ID {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
