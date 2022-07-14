using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Reserva
    {
        public int numReserva { get; set; }  // lo define la BD

        public DateTime fechaI { get; set; }  // lo define el usuario
        public DateTime fechaF { get; set; }  // lo define el usuario
        public string fechaIS { get; set; }
        public string fechaFS { get; set; }
        public int NumHabitacion { get; set; }

        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Nacionalidad { get; set; }

        // suma la tarifa diaria de las habitaciones escogidas, de acuerdo a cantidad de días
        public float PrecioTotal { get; set; }
        public float Descuento { get; set; }
        public float PrecioFinal { get; set; } // lo define la BD, hace el descuento según temporada actual y/o la oferta
        public int NumTarjetaCredito { get; set; }  // a modo simulación, no se guarda en BD

        public int IdOFerta { get; set; }
        public string Temporada { get; set; }

    }

}
