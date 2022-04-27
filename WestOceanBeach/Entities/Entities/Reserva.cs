using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Reserva
    {
        public DateTime fechaI { get; set; }
        public DateTime fechaF { get; set; }
        public string fechaIS { get; set; }
        public string fechaFS { get; set; }
        public int tipoHabitacion { get; set; }
        public int numHabitacion { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string correo { get; set; }
        public string monto { get; set; }
        public int tarjetaCredito { get; set; }

    }// fin clase

}// fin
