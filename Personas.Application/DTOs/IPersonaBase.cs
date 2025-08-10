using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Application.DTOs
{
    public interface IPersonaBase
    {
        string Cedula { get; }
        string Nombres { get; }
        int Edad { get; }
    }
}
