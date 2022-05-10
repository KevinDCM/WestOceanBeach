using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Oferta
    {
        public int cantidad_personas { get; set; }
        public string tipo_habitacion { get; set; }
        public int descuento { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_final { get; set; }

    }
}
