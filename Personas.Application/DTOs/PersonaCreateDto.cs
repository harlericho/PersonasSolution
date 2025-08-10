using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Application.DTOs
{
    public class PersonaCreateDto : IPersonaBase
    {
        public string Cedula { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public int Edad { get; set; } = 0;
        public string Direccion { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;
    }
}
