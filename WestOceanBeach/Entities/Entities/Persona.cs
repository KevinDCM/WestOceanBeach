using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
	public class Persona
	{
		public string Cedula { get; set; }
		public string Nombre { get; set; }
		public string PrimerApellido { get; set; }
		public string SegundoApellido { get; set; }
		public string Correo { get; set; }
		public string Telefono { get; set; }
		public int Edad { get; set; }
		public string Direccion { get; set; }
		public string Nacionalidad { get; set; }
	    public DateTime FechaNacimiento{ get; set; }

	}

}
