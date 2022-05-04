using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Administrador : Persona
    {
        public int IdEmpleado { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public int activo { get; set; }

    }
}
