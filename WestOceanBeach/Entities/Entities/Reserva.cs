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

        // DateTime to string
        public string fechaIS { get; set; }
        public string fechaFS { get; set; }

        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Nacionalidad { get; set; }


        // suma las tarifas diarias de las habitaciones escogidas
        public float CostoSinDescuento { get; set; }
        public float CostoFinal { get; set; } // lo define la BD, hace el descuento según temporada actual y/o la oferta
        public List<Habitacion> Habitaciones { get; set; }  // las define el usuario (funcionalidad tipo carrito compras)
        public string ListHabitaciones { get; set; }
        public int Num_TarjetaCredito { get; set; }  // a modo simulación, no se guarda en BD

        public int IdOFerta { get; set; }

    }

}
