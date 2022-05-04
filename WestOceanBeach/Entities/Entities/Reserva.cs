using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Reserva
    {
        public int numReserva { get; set; }
        public DateTime fechaI { get; set; }
        public DateTime fechaF { get; set; }
        public string fechaIS { get; set; }
        public string fechaFS { get; set; }
        public Cliente Cliente { get; set; }
        public Temporada Temporada { get; set; }
        public Oferta Oferta { get; set; }    // puede ser nula
        public float CostoSinDescuento { get; set; }
        public float CostoFinal { get; set; }
        public List<Habitacion> Habitaciones { get; set; } // una reserva de momento solo permite una habitacion, version 1
        public int Num_TarjetaCredito { get; set; }  // no se guarda en BD
        


        // los siguientes atributos ya están encapsulados (están obsoletos), se quitan en el sprint 3
        public int numHabitacion { get; set; }
        public string tipoHabitacion { get; set; }
        public string nombre { get; set; }

    }

}
