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


        public Cliente Cliente { get; set; }  // lo define el usuario
        public Temporada Temporada { get; set; }  // lo define la BD
        public Oferta Oferta { get; set; }  // puede ser nula (depende de clickear el header)


        // suma las tarifas diarias de las habitaciones escogidas
        public float CostoSinDescuento { get; set; } 

        public float CostoFinal { get; set; } // lo define la BD, hace el descuento según temporada actual y/o la oferta
        public List<Habitacion> Habitaciones { get; set; }  // las define el usuario (funcionalidad tipo carrito compras)
        public int Num_TarjetaCredito { get; set; }  // a modo simulación, no se guarda en BD
        
    }

}
